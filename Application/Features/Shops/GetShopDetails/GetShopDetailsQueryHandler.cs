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
    public class GetShopDetailsQueryHandler : IRequestHandler<GetShopDetailsQuery, ShopDetailsDto>
    {
        private readonly IShopRepository _shopRepository;

        public GetShopDetailsQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public async Task<ShopDetailsDto> Handle(GetShopDetailsQuery request, CancellationToken cancellationToken)
        {
            var shop = await _shopRepository.GetShopById(request.Id) ?? throw new Exception("Shop not found.");

            var shopDto = ShopDetailsDto.CreateShopDetailsDtoFromObject(shop);

            return shopDto;
        }
    }
}
