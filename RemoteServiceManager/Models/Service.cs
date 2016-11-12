using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteServiceManager.Models
{
    public class Service : IService
    {
        public string Name { get; }
        public Service(string name)
        {
            Name = name;
        }
        private ServiceStatus _status = ServiceStatus.Running;
        public ServiceStatus Status => _status;
        public void StatusRequest(ServiceAction action)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            _status = ServiceStatus.Running;
        }

        public void Stop()
        {
            _status = ServiceStatus.Stopped;
        }
    }
}
