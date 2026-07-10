using Architecture.Business.Models.Shareds;

namespace Architecture.Business.Interfaces.Shareds
{
    public interface INotifierService
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
