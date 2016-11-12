using System.Collections.Generic;

namespace RemoteServiceManager.Models
{
    public interface IMachines
    {
        List<IMachine> MachinesList { get; set; }
    }
}