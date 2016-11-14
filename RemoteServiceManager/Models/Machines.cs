using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteServiceManager.Models
{
    public class Machines : IMachines
    {
        public List<IMachine> MachineList { get; set; } = new List<IMachine>();

        public Machines(IOptions<MyOptions> optionsAccessor, IServiceProvider servicesAccessor)
        {
            foreach (var machineName in optionsAccessor.Value.MachineNameList)
            {
                var machine = (IMachine)servicesAccessor.GetService(typeof(IMachine));
                machine.Name = machineName;
                machine.GetServiceStatus();
                MachineList.Add(machine);
            }
        }
    }
}
