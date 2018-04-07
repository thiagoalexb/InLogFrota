using InLogFrota.Impl.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;

namespace InLogFrota.ApresentationWeb.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        private readonly NotificationService _notifications;

        protected BaseController(INotificationHandler<Notification> notifications)
        {
            _notifications = (NotificationService)notifications;
        }

        protected bool IsValidOperation() => (!_notifications.HasNotifications());

        protected new IActionResult Response(string view)
        {
            if (IsValidOperation())
            {
                return Redirect(view);
            }

            foreach (var item in _notifications.GetNotifications())
            {
                ModelState.AddModelError(item.Message, item.Message);
            }
            
            return View();
        }

        protected void NotifyModelStateErrors()
        {
            var values = ModelState.Values.ToList();
            foreach (var value in values)
            {
                var error = value.Errors.FirstOrDefault();
                var erroMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                var key = value.GetType().GetProperty("Key")?.GetValue(value)?.ToString();
                NotifyError(string.Empty, key, erroMsg);
            }
        }

        protected void NotifyError(string code, string source, string message)
        {
            _notifications.Handle(new Notification(message), new CancellationToken());
        }
    }
}
