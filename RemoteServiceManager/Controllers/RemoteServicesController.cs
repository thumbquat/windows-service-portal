using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RemoteServiceManager.Models;

namespace RemoteServiceManager.Controllers
{
    [Route("api/[controller]")]
    public class RemoteServicesController : Controller
    {
        private readonly IMachines _machines;
        public RemoteServicesController(IMachines machines)
        {
            _machines = machines;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Get()
        {
            return Json(_machines);
        }
    }
} 