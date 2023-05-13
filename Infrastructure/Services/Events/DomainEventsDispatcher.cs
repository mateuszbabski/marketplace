using Domain.Shared.Abstractions;
using Infrastructure.Context;
using MediatR;

namespace Infrastructure.Services.Events
{
    internal sealed class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public DomainEventsDispatcher(ApplicationDbContext dbContext, IMediator mediator)
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
