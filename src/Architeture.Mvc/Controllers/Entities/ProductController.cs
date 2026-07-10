using Architeture.Mvc.Controllers.Shareds;
using Microsoft.AspNetCore.Mvc;

namespace Architeture.Mvc.Controllers.Entities
{
    public class ProductController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
