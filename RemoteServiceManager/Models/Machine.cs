using System.Collections.Generic;

namespace RemoteServiceManager.Models
{
    public class Machine : IMachine
    {
     public List<IServices> MachineServiceses { get; set; } = new List<IServices>();
     public string Name { get; set; }
        public Machine(string name)
        {
            Name = name;
        }

    }
}