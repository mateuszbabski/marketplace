using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.Repositories;
using Domain.Shops.Entities.Products.ValueObjects;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal sealed class ProductRepository : IProductRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public ProductRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> Add(Product product)
        {
            await _dbContext.Products.AddAsync(product);

            return product;
        }

        public async Task<Product> GetById(ProductId id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }
    }
}
