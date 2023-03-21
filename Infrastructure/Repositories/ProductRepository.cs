using Domain.Shop.Entities.Products;
using Domain.Shop.Entities.Products.Repositories;
using Domain.Shop.Entities.Products.ValueObjects;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> Add(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product> GetById(ProductId id)
        {
            return await _dbContext.Products.SingleAsync(e => e.Id == id);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _dbContext.Products.AsQueryable().ToListAsync();
        }
    }
}
