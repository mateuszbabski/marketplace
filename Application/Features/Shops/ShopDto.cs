using Domain.Shops;
using Domain.Shops.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shops
{
    public record ShopDto
    {
        public Guid Id { get; init; }
        public string Email { get; init; }
        public string ShopName { get; init; }
        public string ContactNumber { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
        public string Street { get; init; }
        public string PostalCode { get; init; }

        public static ShopDto CreateShopDtoFromObject(Shop shop)
        {
            return new ShopDto()
            {
                Id = shop.Id,
                Email = shop.Email,
                ShopName = shop.ShopName,
                ContactNumber = shop.ContactNumber,
                Country = shop.ShopAddress.Country,
                City = shop.ShopAddress.City,
                Street = shop.ShopAddress.Street,
                PostalCode = shop.ShopAddress.PostalCode
            };
        }
    }
}
