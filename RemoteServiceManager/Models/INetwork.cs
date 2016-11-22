using System;
using System.Collections;
using System.Collections.Generic;

namespace RemoteServiceManager
{
    public interface INetwork
    {
        IEnumerable<Tuple<string, string>> GetServiceStatuses(string machineName);
		IEnumerable<string> GetMachineNames();
    }
}