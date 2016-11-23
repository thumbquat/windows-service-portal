using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RemoteServiceManager.Models;

namespace RemoteServiceManager.Controllers
{
	[Route("api/[controller]")]
	public class RemoteServicesController : Controller
	{
		private readonly INetwork _network;
		public RemoteServicesController(INetwork network)
		{
			_network = network;
		}

		[HttpGet]
		public IActionResult GetMachineNames()
			=> Json(_network.GetServiceStatuses("gilmond-smoke1"));
		//=> Json(_network.GetMachineNames());

		[HttpGet("{machineName}")]
		public IActionResult GetServiceStatuses(string machineName)
			=> Json(_network.GetServiceStatuses(machineName));

		[HttpGet("{machineName}/{serviceName}/{serviceAction}")]
		public IActionResult ChangeServiceStatus(string machineName, string serviceName, string serviceAction)
		{
			return Json(_network.ChangeServiceStatus(machineName, serviceName, serviceAction));
		}
	}
}