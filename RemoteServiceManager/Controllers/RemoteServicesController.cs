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

		[HttpGet("machineNames")]
		public IActionResult GetMachineNames()
			=> Json(_network.GetMachineNames());

		[HttpGet("serviceNames")]
		public IActionResult GetServiceNames()
			=> Json(_network.GetServiceNames());

		[HttpGet("status/{machineName}")]
		public IActionResult GetServiceStatuses(string machineName)
			=> Json(_network.GetServiceStatuses(machineName));

		[HttpGet("changeservice/{machineName}/{serviceName}/{serviceAction}")]
		public IActionResult ChangeServiceStatus(string machineName, string serviceName, string serviceAction)
			=> Json(_network.ChangeServiceStatus(machineName, serviceName, serviceAction));
	}
}