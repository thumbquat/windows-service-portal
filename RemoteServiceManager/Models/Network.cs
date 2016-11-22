using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace RemoteServiceManager.Models
{
    public class Network : INetwork
    {
        private readonly IEnumerable<string> _machineNames;
        private readonly IEnumerable<string> _serviceNames;

        public Network(IOptions<MyOptions> options)
        {
            _machineNames = options.Value.MachineNameList;
            _serviceNames = options.Value.ServiceNameList;
        }
        public IEnumerable<string> GetMachineNames()
            => _machineNames;

        public IEnumerable<Tuple<string, string>> GetServiceStatuses(string machineName)
        {
            var statuses = new List<Tuple<string, string>>();
            foreach (var serviceName in _serviceNames)
            {
                statuses.Add(Tuple.Create(serviceName, await GetServiceStatus(machineName, serviceName)));
            }
            return statuses;
        }

        private Task<string> GetServiceStatus(string machineName, string serviceName)
        {
            var tcs = new TaskCompletionSource<string>();
            try
            {
                using (var serviceController = new ServiceController(serviceName, machineName))
                {
                    tcs.SetResult(serviceController.Status.ToString());
                }

            }
            catch (InvalidOperationException)
            {
                tcs.SetResult("Not Installed");
            }
            return tcs.Task;
        }
    }
}
