using Domain.Shared.Abstractions;
using MediatR;

namespace Application.Common.Services.DomainEvents
{
    public class DomainEventHandler<T> : IDomainEventHandler<T> where T : IDomainEvent
    {
        public T DomainEvent { get; }

        public DomainEventHandler(T domainEvent)
        {
            this.DomainEvent = domainEvent;
        }
    }
}
