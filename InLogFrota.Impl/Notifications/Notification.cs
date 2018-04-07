using MediatR;

namespace InLogFrota.Impl.Notifications
{
    public class Notification : INotification
    {
        public string Message { get; private set; }

        public Notification(string message) => 
            Message = message;
    }
}
