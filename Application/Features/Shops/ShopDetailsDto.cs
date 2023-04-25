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
        public Guid Id { get; init; }
        public string Email { get; init; }
        public string OwnerName { get; init; }
        public string OwnerLastName { get; init; }
        public string ShopName { get; init; }
        public string TaxNumber { get; init; }
        public string ContactNumber { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
        public string Street { get; init; }
        public string PostalCode { get; init; }
        public List<ProductDto> ProductList { get; init; }

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
