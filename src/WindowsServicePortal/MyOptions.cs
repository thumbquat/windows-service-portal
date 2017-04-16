using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindowsServicePortal.Models;

namespace WindowsServicePortal
{
    public class MyOptions
    {
        public List<Machine> Machines { get; set; }
        public List<Service> Services { get; set; }
    }
}
