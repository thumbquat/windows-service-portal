using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WindowsServicePortal.Models;
using System.Collections.Concurrent;

namespace WindowsServicePortal.Controllers
{
    [Route("api/[controller]")]
    public class WindowsService : Controller
    {
        private readonly INetwork _network;
        public WindowsService(INetwork network)
        {
            _network = network;
        }

        [HttpGet("machineNames")]
        public IActionResult GetMachineNames()
            => Json(_network.GetMachineNames()
                .Select(name => new { MachineName = name }));

        [HttpGet("status/{machineName}")]
        public IActionResult GetServiceStatuses(string machineName)
            => Json(_network.GetServiceStatuses(machineName)
                .Select(x => new { MachineName = machineName, ServiceName = x.Key, Status = x.Value }));

        [HttpGet("action/{serviceAction}/{machineName}")]
        public IActionResult ChangeAllServices(ServiceAction serviceAction, string machineName)
            => Json(ServiceActionToMachine(serviceAction, machineName)
                .Select(x => new
                {
                    MachineName = machineName,
                    ServiceName = x.Key,
                    ServiceAction = serviceAction.ToString(),
                    Succeeded = x.Value
                }));

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
            _network.GetServiceNames().AsParallel()
                .ForAll(s => results.TryAdd(s, _network.ChangeServiceStatus(machineName, s, action)));
            return results;
        }
    }
}