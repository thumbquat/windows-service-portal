using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace RemoteServiceManager.Models
{
    public class Machines : IMachines
    {
        public List<IMachine> MachinesList { get; set; } = new List<IMachine>();
        public Machines(IOptions<MyOptions> myOptions)
        {
            foreach (var machineName in myOptions.Value.MachineNameList)
            {
                MachinesList.Add(new Machine(machineName));
            }
        }
    }
}