using Architecture.Business.Interfaces.Shareds;
using Microsoft.AspNetCore.Mvc;

namespace Architeture.Mvc.Controllers.Shareds
{
    public abstract class BaseController : Controller
    {
        private readonly INotifierService _notifier;

        protected BaseController(INotifierService notifier)
        {
            _notifier = notifier;
        }
        protected bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }
    }
}
