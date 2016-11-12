using System.Collections.Generic;

namespace RemoteServiceManager.Models
{
    public interface IServices
    {
        List<IService> ServicesList { get; set; }
    }
}