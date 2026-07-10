using Architecture.Business.Interfaces.Shareds;
using Microsoft.AspNetCore.Mvc;

namespace Architeture.Mvc.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotifierService _notifier;

        public SummaryViewComponent(INotifierService notifier)
        {
            _notifier = notifier;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notifier.GetNotifications());
            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Message));

            return View();
        }
    }
}
