using System;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteServiceManager.Models
{
    public interface IService
    {
        string Name { get; set; }
        ServiceStatus Status { get; }
        void StatusRequest(ServiceAction action);
    }
}
