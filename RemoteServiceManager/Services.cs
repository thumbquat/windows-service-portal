using System.Collections.Generic;
using RemoteServiceManager.Models;

namespace RemoteServiceManager
{
    public class Services : IServices
    {
        public List<IService> ServicesList { get; set; }
    }
}