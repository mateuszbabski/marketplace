using Domain.Shops.Entities.Products.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shops.Entities.Products.Repositories
{
    public interface IProductRepository
    {
        Task<Products> Add(Products product);
        Task Update(Products product);
        Task<Products> GetById(ProductId id);
        Task<List<Products>> GetAllProducts();
    }
}
