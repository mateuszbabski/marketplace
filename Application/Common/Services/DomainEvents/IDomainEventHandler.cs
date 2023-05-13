using MediatR;

namespace Application.Common.Services.DomainEvents
{
    public interface IDomainEventHandler<out TEventType> : IDomainEventHandler
    {
        TEventType DomainEvent { get; }
    }
    public interface IDomainEventHandler : INotification
    {
    }
}
