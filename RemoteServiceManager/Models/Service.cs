using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteServiceManager.Models
{
    public class Service
    {
        private ServiceStatus _status = ServiceStatus.Running;
        public ServiceStatus Status
        {
            get
            {
                return _status;
            }
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
