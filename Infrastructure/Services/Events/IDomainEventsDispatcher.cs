namespace Infrastructure.Services.Events
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
