using Architecture.Business.Models.Shareds;

namespace Architecture.Business.Interfaces.Shareds
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
