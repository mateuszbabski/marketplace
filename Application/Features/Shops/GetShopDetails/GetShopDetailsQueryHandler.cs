using Domain.Customers;
using Domain.Shops.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shops.GetShopDetails
{
    public class GetShopDetailsQueryHandler : IRequestHandler<GetShopDetailsQuery, ShopDto>
    {
        private readonly IShopRepository _shopRepository;

        public GetShopDetailsQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public async Task<ShopDto> Handle(GetShopDetailsQuery request, CancellationToken cancellationToken)
        {
            var shop = await _shopRepository.GetShopById(request.Id) ?? throw new Exception("Shop not found.");

            var shopViewModel = new ShopDto()
            {
                Id = shop.Id,
                Email = shop.Email,
                OwnerName = shop.OwnerName,
                OwnerLastName = shop.OwnerLastName,
                ShopName = shop.ShopName,
                TaxNumber = shop.TaxNumber,
                ContactNumber = shop.ContactNumber,
                Country = shop.ShopAddress.Country,
                City = shop.ShopAddress.City,
                PostalCode = shop.ShopAddress.PostalCode,
                Street = shop.ShopAddress.Street
            };

            return shopViewModel;
        }
    }
}
