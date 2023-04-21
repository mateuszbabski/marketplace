using Domain.Shops.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shops.GetAllShops
{
    public class GetAllShopsQueryHandler : IRequestHandler<GetAllShopsQuery, IEnumerable<ShopDto>>
    {
        private readonly IShopRepository _shopRepository;

        public GetAllShopsQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public async Task<IEnumerable<ShopDto>> Handle(GetAllShopsQuery request, CancellationToken cancellationToken)
        {
            var shops = await _shopRepository.GetAllShops();

            var shopListDto = new List<ShopDto>();

            foreach (var shop in shops)
            {
                var shopDto = ShopDto.CreateShopDtoFromObject(shop);

                shopListDto.Add(shopDto);
            }

            return shopListDto.AsEnumerable();
        }
    }
}
