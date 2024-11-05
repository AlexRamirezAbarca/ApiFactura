using Microsoft.AspNetCore.Mvc;

namespace ApiFactura.Controllers
{
    public class BillsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
