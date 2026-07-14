using Architecture.Business.Interfaces.Shareds;
using Architecture.Business.Models.Shareds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Architecture.API.Controllers.Shareds
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly INotifierService _notifier;

        protected BaseController(INotifierService notifier)
        {
            _notifier = notifier;
        }

        protected bool ValidOperation()
        {
            var notifier = !_notifier.HasNotification();
            return notifier;
        }

        protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object result = null)
        {
            if (ValidOperation())
            {
                return new ObjectResult(result)
                {
                    StatusCode = Convert.ToInt32(statusCode),
                };
            }

            return BadRequest(new
            {
                errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyInvalidModelError(modelState);
            return CustomResponse();
        }

        protected void NotifyInvalidModelError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifierError(errorMsg);
            }
        }

        protected void NotifierError(string message)
        {
            _notifier.Handle(new Notification(message));
        }
    }
}
