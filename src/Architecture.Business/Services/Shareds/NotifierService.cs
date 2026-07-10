using Architecture.Business.Interfaces.Shareds;
using Architecture.Business.Models.Shareds;

namespace Architecture.Business.Services.Shareds
{
    public class NotifierService : INotifierService
    {
        private List<Notification> _notifications;

        public NotifierService()
        {
            _notifications = new List<Notification>();
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}
