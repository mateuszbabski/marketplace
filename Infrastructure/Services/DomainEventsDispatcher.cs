using Domain.Shared.Abstractions;
using Infrastructure.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    internal sealed class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Mediator _mediator;

        public DomainEventsDispatcher(ApplicationDbContext dbContext, Mediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEntities = _dbContext.ChangeTracker.Entries<Entity>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity)
                .ToList();

            var domainEvents = domainEntities.SelectMany(x => x.DomainEvents).ToList();

            if (domainEvents is null) 
                return;

            domainEntities.ForEach(x => x.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
