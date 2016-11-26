using WindowsServicePortal.Models;
using System.Collections.Generic;

namespace WindowsServicePortal
{
	public interface INetwork
	{
		IDictionary<string, string> GetServiceStatuses(string machineName);
		IEnumerable<string> GetMachineNames();
		IEnumerable<string> GetServiceNames();
		bool ChangeServiceStatus(string machineName, string serviceName, ServiceAction serviceAction);
	}
}