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

		bool ChangeServiceStatus(string machineName, string serviceName, ServiceAction serviceAction);

		bool StartService(string servicename, string machineName);
		bool StopService(string servicename, string machineName);
		bool RestartService(string servicename, string machineName);
	}
}