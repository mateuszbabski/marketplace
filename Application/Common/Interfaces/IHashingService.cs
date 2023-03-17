namespace Application.Common.Interfaces
{
    public interface IHashingService
    {
        string GenerateSalt();
        string GenerateHashPassword(string password);
        bool ValidatePassword(string password, string correctHash);
    }
}
