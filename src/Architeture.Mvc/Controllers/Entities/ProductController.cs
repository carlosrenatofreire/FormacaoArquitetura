using Architecture.Business.Interfaces.Internals.Entities;
using Architecture.Business.Interfaces.Shareds;
using Architeture.Mvc.Controllers.Shareds;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Architeture.Mvc.Controllers.Entities
{
    public class ProductController : BaseController
    {
        public ProductController(INotifierService notifier) : base(notifier) { }

        public IActionResult Index()
        {
            return View();
        }
    }
}
