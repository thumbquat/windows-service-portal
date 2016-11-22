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
            Parallel.ForEach(_serviceNames, (serviceName) =>
                statuses.Add(Tuple.Create(serviceName, GetServiceStatus(machineName, serviceName))));
            return statuses;
        }

        private string GetServiceStatus(string machineName, string serviceName)
        {
            var serviceStatus = string.Empty;
            try
            {
                using (var serviceController = new ServiceController(serviceName, machineName))
                {
                    serviceStatus = serviceController.Status.ToString();
                }

            }
            catch (InvalidOperationException)
            {
                serviceStatus = "Not Installed";
            }
            return serviceStatus;
        }
    }
}
