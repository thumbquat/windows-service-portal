using System;
using WindowsServicePortal;

namespace WindowsServicePortal.Models
{
    public class RealServiceControllerFactory : IServiceControllerFactory
    {
        public IServiceController GetServiceController(string machineName, string serviceName)
            => new RealServiceController(machineName, serviceName);
    }
}