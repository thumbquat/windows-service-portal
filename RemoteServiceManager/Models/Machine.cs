using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

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
                var service = (IService)servicesAccessor.GetService(typeof(IService));
                service.Name = serviceName;
                MachineServices.Add(service);
            }
        }
    }
}