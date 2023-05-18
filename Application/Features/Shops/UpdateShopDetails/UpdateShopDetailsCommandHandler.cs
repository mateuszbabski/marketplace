using Application.Common.Interfaces;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products.Repositories;
using Domain.Shops.Repositories;
using Domain.Shops.ValueObjects;
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
    public class UpdateShopDetailsCommandHandler : IRequestHandler<UpdateShopDetailsCommand>
    {
        private readonly ICurrentUserService _userService;
        private readonly IShopRepository _shopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateShopDetailsCommandHandler(ICurrentUserService userService,
                                               IShopRepository shopRepository,
                                               IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _shopRepository = shopRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateShopDetailsCommand request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;
            var shop = await _shopRepository.GetShopById(request.Id);

            if (shop == null || shop.Id.Value != userId)
            {
                throw new Exception("Shop not found");
            }

            shop.UpdateShopDetails(request.OwnerName,
                                   request.OwnerLastName,
                                   request.ShopName,
                                   request.Country,
                                   request.City,
                                   request.Street,
                                   request.PostalCode,
                                   request.TaxNumber,
                                   request.ContactNumber);  

            await _unitOfWork.CommitAsync();
        }
    }
}
