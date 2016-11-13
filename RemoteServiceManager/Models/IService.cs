using System;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace RemoteServiceManager.Models
{
    public interface IService
    {
        string Name { get; set; }
        ServiceControllerStatus Status { get; }
        void StatusRequest(ServiceAction action);
    }
}
