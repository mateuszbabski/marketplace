using Application.Common.Interfaces;
using Domain.Customers.Entities.Orders.Repositories;
using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.Repositories;
using Domain.Shops.Entities.ShopOrders.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.CancelOrder
{
    public class CancelOrderQueryCommand : IRequestHandler<CancelOrderCommand, Unit>
    {
        private readonly ICurrentUserService _userService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IShopOrderRepository _shopOrderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelOrderQueryCommand(ICurrentUserService userService,
                                       ICustomerRepository customerRepository,
                                       IShopOrderRepository shopOrderRepository,
                                       IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _customerRepository = customerRepository;
            _shopOrderRepository = shopOrderRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var customer = await _customerRepository.GetCustomerById(customerId);

            customer.CancelOrder(request.Id);

            CancelRelatedShopOrders(request.Id);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async void CancelRelatedShopOrders(Guid Id)
        {
            var shopOrderList = await _shopOrderRepository.GetShopOrdersByOrderId(Id);

            foreach(var order in shopOrderList)
            {
                order.CancelOrder();
            }
        }
    }
}
