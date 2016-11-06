using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteServiceManager.Models
{
    public enum ServiceStatus
    {
            NotInstalled,
            Stopped,
            Starting,
            Running,
            Stopping
    }
}
