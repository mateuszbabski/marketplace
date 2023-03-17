using Domain.Entrepreneur;
using Domain.Entrepreneur.ValueObjects;

namespace Infrastructure.Repositories
{
    public interface IEntrepreneurRepository
    {
        Task<Entrepreneur> Add(Entrepreneur entrepreneur);
        Task<Entrepreneur> GetEntrepreneurByEmail(string email);
        Task<Entrepreneur> GetEntrepreneurById(EntrepreneurId id);
    }
}