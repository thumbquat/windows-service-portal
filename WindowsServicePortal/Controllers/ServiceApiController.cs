using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WindowsServicePortal.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WindowsServicePortal.Controllers
{
    [Route("api/[controller]")]
    public class WindowsServiceApi : Controller
    {
        private readonly INetwork _network;
        public WindowsServiceApi(INetwork network)
        {
            _network = network;
        }

        [HttpGet]
        public IActionResult Index()
            => View();

        [HttpGet("machineNames")]
		public IActionResult GetMachineNames()
			=> Json(_network.GetMachineNames());

		[HttpGet("serviceNames")]
		public IActionResult GetServiceNames()
			=> Json(_network.GetServiceNames());

		[HttpGet("status/{machineName}")]
		public IActionResult GetServiceStatuses(string machineName)
			=> Json(_network.GetServiceStatuses(machineName));

		[HttpGet("action/{serviceAction}/{machineName}")]
		public IActionResult ChangeAllServices(ServiceAction serviceAction, string machineName)
			=> Json(ServiceActionToMachine(serviceAction, machineName));

		[HttpGet("action/{serviceAction}/{machineName}/{serviceName}")]
		public IActionResult StopService(ServiceAction serviceAction, string machineName, string serviceName)
			=> Json(new KeyValuePair<string, bool>(serviceName, _network.ChangeServiceStatus(machineName, serviceName, serviceAction)));

		private ConcurrentDictionary<string, bool> ServiceActionToMachine(ServiceAction action, string machineName)
		{
			var results = new ConcurrentDictionary<string, bool>();
			_network.GetServiceNames().AsParallel()
				.ForAll(s => results.TryAdd(s, _network.ChangeServiceStatus(machineName, s, action)));
			return results;
		}
	}
}