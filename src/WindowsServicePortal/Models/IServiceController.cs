using System;
using System.ServiceProcess;

namespace WindowsServicePortal.Models
{
    public interface IServiceController : IDisposable
    {
        bool CanStop();
        ServiceControllerStatus GetStatus();
        void Stop();
        void Start();
        void WaitForStatus(ServiceControllerStatus status);
    }
}