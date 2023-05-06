using Domain.Shared.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
