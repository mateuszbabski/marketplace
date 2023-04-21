using Application.Features.Products;
using Domain.Shops;
using Domain.Shops.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shops
{
    public record ShopDetailsDto
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string OwnerName { get; private set; }
        public string OwnerLastName { get; private set; }
        public string ShopName { get; private set; }
        public string TaxNumber { get; private set; }
        public string ContactNumber { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string PostalCode { get; private set; }
        public List<ProductDto> ProductList { get; private set; }

        public static ShopDetailsDto CreateShopDetailsDtoFromObject(Shop shop)
        {
            var shopProductList = new List<ProductDto>();

            foreach (var product in shop.ProductList)
            {
                var productDto = ProductDto.CreateProductDtoFromObject(product);

                shopProductList.Add(productDto);
            }

            return new ShopDetailsDto()
            {
                Id = shop.Id,
                Email = shop.Email,
                ShopName = shop.ShopName,
                ContactNumber = shop.ContactNumber,
                Country = shop.ShopAddress.Country,
                City = shop.ShopAddress.City,
                Street = shop.ShopAddress.Street,
                PostalCode = shop.ShopAddress.PostalCode,
                OwnerLastName = shop.OwnerLastName,
                OwnerName = shop.OwnerName,
                TaxNumber = shop.TaxNumber,
                ProductList = shopProductList
            };
        }
    }
}
