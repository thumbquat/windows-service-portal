using Microsoft.AspNetCore.Mvc;

namespace WindowsServicePortal.Controllers
{
	[Route("")]
	public class PortalController : Controller
	{
		// GET: /<controller>/
		public IActionResult React()
			=> View();
	}
}
