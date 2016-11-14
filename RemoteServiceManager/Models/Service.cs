using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace RemoteServiceManager.Models
{
	public class Service : IService
	{
		public string Name { get; set; }
		public string MachineName { get; set; }
		public string Status { get; set; }
		public void StatusRequest(ServiceAction action)
		{
			throw new NotImplementedException();
		}

		public void Start()
		{
			throw new NotImplementedException();
		}

		public void Stop()
		{
			throw new NotImplementedException();
		}

		public void GetStatus()
		{
			using (var actualService = new ServiceController(Name, MachineName))
			{
				try
				{
					Status = actualService.Status.ToString();
				}
				catch (InvalidOperationException)
				{

					Status = "Not Installed";
				}

			}
		}
	}
}
