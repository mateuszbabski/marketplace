using Domain.Entrepreneur;
using Domain.Entrepreneur.Repositories;
using Domain.Entrepreneur.ValueObjects;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EntrepreneurRepository : IEntrepreneurRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EntrepreneurRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Entrepreneur> Add(Entrepreneur entrepreneur)
        {
            await _dbContext.Entrepreneurs.AddAsync(entrepreneur);
            await _dbContext.SaveChangesAsync();

            return entrepreneur;
        }

        public async Task<Entrepreneur> GetEntrepreneurByEmail(string email)
        {
            return await _dbContext.Entrepreneurs.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Entrepreneur> GetEntrepreneurById(EntrepreneurId id)
        {
            return await _dbContext.Entrepreneurs.SingleAsync(e => e.Id == id);
        }
    }
}
