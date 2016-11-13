using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace RemoteServiceManager.Models
{
    public class Service : IService
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                _actualService = new ServiceController(value);
                _status = _actualService.Status;
            }
        }
        private ServiceController _actualService;

        private ServiceControllerStatus _status;
        public ServiceControllerStatus Status => _status;
        public void StatusRequest(ServiceAction action)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
