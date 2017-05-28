using WindowsServicePortal.Models;
using System;
using System.ServiceProcess;

namespace WindowsServicePortal.Tests.Fakes
{
    public class FakeServiceController : IServiceController
    {
        public bool CanStop() => true;
        public ServiceControllerStatus GetStatus()
        {
            throw new NotImplementedException();
        }
        public void Stop()
        {
            
        }
        public void Start()
        {}
        public void WaitForStatus(ServiceControllerStatus status)
        {}
        public void Dispose()
        {}
    }
}