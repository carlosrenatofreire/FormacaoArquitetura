using Microsoft.AspNetCore.Mvc;

namespace Architeture.Mvc.Controllers
{
    public class SupplierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
