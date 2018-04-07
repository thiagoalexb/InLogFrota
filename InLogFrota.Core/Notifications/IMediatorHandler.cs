using MediatR;
using System.Threading.Tasks;

namespace InLogFrota.Core.Notifications
{
    public interface IMediatorHandler
    {
        Task RaiseEvent<T>(T @event) where T : INotification;
    }
}
