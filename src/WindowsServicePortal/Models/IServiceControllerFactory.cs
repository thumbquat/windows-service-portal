using System;

namespace WindowsServicePortal.Models
{
    public interface IServiceControllerFactory
    {
        IServiceController GetServiceController(string machineName, string serviceName);
    }
}