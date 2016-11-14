using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RemoteServiceManager.Models
{
    public class Machine : IMachine
    {
        public string Name { get; set; }
        public List<IService> MachineServices { get; set; } = new List<IService>();

        public Machine(IOptions<MyOptions> optionsAccessor, IServiceProvider servicesAccessor)
        {
            foreach (var serviceName in optionsAccessor.Value.ServiceNameList)
            {
				var service = servicesAccessor.GetService<IService>();
				service.Name = serviceName;
                MachineServices.Add(service);
            }
        }

        public void GetServiceStatus()
        {
            foreach (var service in MachineServices)
            {
                service.MachineName = Name;
                service.GetStatus();
            }
        }
    }
}