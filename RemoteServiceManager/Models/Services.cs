using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace RemoteServiceManager.Models
{
    public class Services : IServices
    {
        public List<IService> ServicesList { get; set; } = new List<IService>();
        public Services(IOptions<MyOptions> myOptions )
        {
            foreach (var serviceName in myOptions.Value.ServiceNameList)
            {
                ServicesList.Add(new Service(serviceName));
            } 
        }
    }
}
