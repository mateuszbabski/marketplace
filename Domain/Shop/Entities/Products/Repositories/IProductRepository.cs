using Domain.Shop.Entities.Products.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop.Entities.Products.Repositories
{
    public interface IProductRepository
    {
        Task<Product> Add(Product product);
        Task<Product> GetById(ProductId id);
        Task<List<Product>> GetAllProducts();
    }
}
