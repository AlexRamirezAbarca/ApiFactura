using Microsoft.AspNetCore.Mvc;

namespace ApiFactura.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
