using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WindowsServicePortal.Models;
using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace WindowsServicePortal.Controllers
{
	[Route("api/[controller]")]
	public class WindowsService : Controller
	{
		private readonly INetwork _network;
		private readonly IMemoryCache _memoryCache;
		public WindowsService(INetwork network, IMemoryCache memoryCache)
		{
			_network = network;
			_memoryCache = memoryCache;
		}

		[HttpGet("machines")]
		public IActionResult GetMachines()
			=> Json(_network.GetMachines()
				.Select(name => new { Name = name }));

		[HttpGet("status/{machineName}")]
		public IActionResult GetServiceStatuses(string machineName)
		{
			var cacheKey = $"status_{machineName}";
			IActionResult result;
			if (_memoryCache.TryGetValue(cacheKey, out result))
				return result;
			else
			{
				result = Json(_network.GetServiceStatuses(machineName)
					   .Select(x => new { Name = x.Key, Status = x.Value, MachineName = machineName})
					   .OrderBy(x => x.Name));
				_memoryCache.Set(cacheKey, result,
					new MemoryCacheEntryOptions()
						.SetAbsoluteExpiration(TimeSpan.FromSeconds(2)));
				return result;
			}
		}

		[HttpGet("action/{serviceAction}/{machineName}")]
		public IActionResult ChangeAllServices(ServiceAction serviceAction, string machineName)
			=> Json(ServiceActionToMachine(serviceAction, machineName)
				.Select(x => new
				{
					ServiceName = x.Key,
					ServiceAction = serviceAction.ToString(),
					Succeeded = x.Value
				})
				.OrderBy(x => x.ServiceName));

		[HttpGet("action/{serviceAction}/{machineName}/{serviceName}")]
		public IActionResult ChangeService(ServiceAction serviceAction, string machineName, string serviceName)
			=> Json(new
			{
				MachineName = machineName,
				ServiceName = serviceName,
				ServiceAction = serviceAction.ToString(),
				Succeeded = _network.ChangeServiceStatus(machineName, serviceName, serviceAction)
			});

		private ConcurrentDictionary<string, bool> ServiceActionToMachine(ServiceAction action, string machineName)
		{
			var results = new ConcurrentDictionary<string, bool>();
			_network.GetServices().AsParallel()
				.ForAll(service => results.TryAdd(service.Name, _network.ChangeServiceStatus(machineName, service.Name, action)));
			return results;
		}
	}
}