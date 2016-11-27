using Microsoft.AspNetCore.Mvc;

namespace WindowsServicePortal.Controllers
{
    [Route("[controller]")]
    public class PortalController : Controller
    {
        // GET: /<controller>/
        public IActionResult Home()
            =>View();
    }
}
