using System.Collections.Generic;

namespace RemoteServiceManager.Models
{
    public interface IMachine
    {
        string Name { get; set; }
        List<IService> MachineServices { get; set; }

        void Initialize();
    }
}