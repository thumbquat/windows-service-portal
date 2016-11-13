using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace RemoteServiceManager.Models
{
    public class Service : IService
    {
        public string Name { get; set; }
        public string MachineName { get; set; }
        private ServiceController _actualService;

        private ServiceControllerStatus _status;
        public String Status => _status.ToString();
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

        public void Initialize()
        {
            _actualService = new ServiceController(Name, MachineName);
            _status = _actualService.Status;
        }
    }
}
