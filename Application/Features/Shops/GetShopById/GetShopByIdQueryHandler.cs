using Domain.Shops.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shops.GetShopById
{
    public class GetShopByIdQueryHandler : IRequestHandler<GetShopByIdQuery, ShopDetailsDto>
    {
        private readonly IShopRepository _shopRepository;

        public GetShopByIdQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public async Task<ShopDetailsDto> Handle(GetShopByIdQuery request, CancellationToken cancellationToken)
        {
            var shop = await _shopRepository.GetShopById(request.Id) ?? throw new Exception("Shop not found.");

            var shopDto = ShopDetailsDto.CreateShopDetailsDtoFromObject(shop);

            return shopDto;
        }
    }
}
