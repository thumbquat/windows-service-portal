using Microsoft.AspNetCore.Mvc;

namespace WindowsServicePortal.Controllers
{
	[Route("")]
	public class PortalController : Controller
	{
        [HttpGet("")]
        [HttpGet("{machineName}")]
        public IActionResult React()
			=> View();
	}
}
