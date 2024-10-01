using Microsoft.AspNetCore.Mvc;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncansController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
