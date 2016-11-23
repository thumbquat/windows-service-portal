using RemoteServiceManager.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RemoteServiceManager
{
    public interface INetwork
    {
        IEnumerable<Tuple<string, string>> GetServiceStatuses(string machineName);
		IEnumerable<string> GetMachineNames();

		IEnumerable<string> GetServiceNames();

		bool ChangeServiceStatus(string machineName, string serviceName, string serviceAction);
    }
}