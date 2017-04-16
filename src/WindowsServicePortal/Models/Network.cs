using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;

namespace WindowsServicePortal.Models
{
	public class Network : INetwork
	{
		private readonly IEnumerable<Machine> _machines;
		private readonly IEnumerable<Service> _services;

		public Network(IOptions<MyOptions> options)
		{
			_machines = options.Value.Machines;
			_services = options.Value.Services;
		}

		public bool ChangeServiceStatus(string machineName, string serviceName, ServiceAction serviceAction)
		{
			switch (serviceAction)
			{
				case ServiceAction.Start:
					return StartService(serviceName, machineName);
				case ServiceAction.Stop:
					return StopService(serviceName, machineName);
				case ServiceAction.Restart:
					return RestartService(serviceName, machineName);
				default:
					return false;
			}
		}

		public IEnumerable<Machine> GetMachines()
			=> _machines;

		public IEnumerable<Service> GetServices()
			=> _services;

		public IDictionary<string, string> GetServiceStatuses(string machineName)
		{
			var statuses = new ConcurrentDictionary<string, string>();
			_services.AsParallel().ForAll(service => statuses.TryAdd(service.Name, GetServiceStatus(machineName, service.Name)));
			return statuses;
		}

		private bool RestartService(string servicename, string machineName)
			=> StopService(servicename, machineName) && StartService(servicename, machineName);

		private bool StartService(string servicename, string machineName)
		{
			try
			{
				using (var service = new ServiceController(servicename, machineName))
				{
					service.Start();
					service.WaitForStatus(ServiceControllerStatus.Running);
					return true;
				}
			}
			catch (InvalidOperationException)
			{
				return false;
			}

		}
		private bool StopService(string servicename, string machineName)
		{
			try
			{
				using (var service = new ServiceController(servicename, machineName))
				{
					if (service.CanStop)
					{
						service.Stop();
						service.WaitForStatus(ServiceControllerStatus.Stopped);
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			catch (InvalidOperationException)
			{
				return false;
			}
		}

		private string GetServiceStatus(string machineName, string serviceName)
		{
			var serviceStatus = string.Empty;
			try
			{
				using (var serviceController = new ServiceController(serviceName, machineName))
				{
					serviceStatus = serviceController.Status.ToString();
				}
			}
			catch (InvalidOperationException)
			{
				serviceStatus = "Not Installed";
			}
			return serviceStatus;
		}
	}
}
