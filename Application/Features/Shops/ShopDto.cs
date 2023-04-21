﻿using Domain.Shops;
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
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string ShopName { get; private set; }
        public string ContactNumber { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string PostalCode { get; private set; }

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
