using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteServiceManager.Models
{
    public enum ServiceAction
    {
        Stop,
        Restart,
        Start
    }
    public class RemoteMachine
    {
        private string _name;
        private List<Service> _services;
        public string Name { get; private set; }

        public RemoteMachine(string name)
        {
            _name = name;
            _services = new List<Service>();
        }

        public void AddService(Service service)
        {
            _services.Add(service);
        }

        public void IndividualServiceAction(Service service, ServiceAction action)
        {
            throw new NotImplementedException();
        }

        public void AllServicesAction(ServiceAction action)
        {
            throw new NotImplementedException();
        }

        public List<Service> GetServices()
        {
            throw new NotImplementedException();
        }
    }
}
