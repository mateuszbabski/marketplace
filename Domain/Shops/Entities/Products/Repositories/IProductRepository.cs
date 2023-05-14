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
        Task<Product> Add(Product product);
        void Update(Product product);
        Task<Product> GetById(ProductId id);
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
