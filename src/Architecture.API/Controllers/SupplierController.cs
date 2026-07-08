using Microsoft.AspNetCore.Mvc;

namespace Architecture.API.Controllers
{
    public class SupplierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
