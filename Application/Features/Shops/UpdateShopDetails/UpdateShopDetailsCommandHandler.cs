using Application.Common.Interfaces;
using Domain.Shared.ValueObjects;
using Domain.Shop.Entities.Products.Repositories;
using Domain.Shop.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shops.UpdateShopDetails
{
    public class UpdateShopDetailsCommandHandler : IRequestHandler<UpdateShopDetailsCommand, Unit>
    {
        private readonly ICurrentUserService _userService;
        private readonly IShopRepository _shopRepository;

        public UpdateShopDetailsCommandHandler(ICurrentUserService userService, IShopRepository shopRepository)
        {
            _userService = userService;
            _shopRepository = shopRepository;
        }
        public async Task<Unit> Handle(UpdateShopDetailsCommand request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;
            var shop = await _shopRepository.GetShopById(request.Id);

            if (shop == null || shop.Id.Value != userId)
            {
                throw new Exception("Shop not found");
            }

            var addressParams = new List<string>()
            {
                request.Country, request.City, request.Street, request.PostalCode
            };

            if (addressParams.All(c => !string.IsNullOrEmpty(c)))
            {
                var newShopAddress = Address.CreateAddress(request.Country, request.City, request.Street, request.PostalCode);
                shop.SetAddress(newShopAddress);
            }                

            shop.SetShopName(request.ShopName);
            shop.SetOwnerLastName(request.OwnerLastName);
            shop.SetOwnerName(request.OwnerName);
            shop.SetContactNumber(request.ContactNumber);
            shop.SetTaxNumber(request.TaxNumber);            

            await _shopRepository.Update(shop);
            return Unit.Value;
        }
    }
}
