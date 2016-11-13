using System;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace RemoteServiceManager.Models
{
    public interface IService
    {
        string Name { get; set; }
        string MachineName { get; set; }
        string Status { get; }
        void StatusRequest(ServiceAction action);
        void Initialize();
    }
}
