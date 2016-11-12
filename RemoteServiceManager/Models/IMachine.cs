using System.Collections.Generic;

namespace RemoteServiceManager.Models
{
    public interface IMachine
    {
        string Name { get; set; }
        List<IServices> MachineServiceses { get; set; }
    }
}