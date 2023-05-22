namespace Domain.Shared.Abstractions
{
    public interface IBusinessRule
    {
        bool IsBroken();
        string Message { get; }
    }
}
