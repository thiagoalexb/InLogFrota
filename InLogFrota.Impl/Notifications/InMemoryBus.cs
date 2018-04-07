using InLogFrota.Core.Notifications;
using MediatR;
using System.Threading.Tasks;

namespace InLogFrota.Impl.Notifications
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator) => _mediator = mediator;

        public async Task RaiseEvent<T>(T @event) where T : INotification =>
             await Publish(@event);

        private async Task Publish<T>(T message) where T : INotification =>
            await _mediator.Publish(message);
    }
}
