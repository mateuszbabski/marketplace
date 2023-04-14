//using Application.Common.Interfaces;
//using Domain.Customers.Entities.Orders.Repositories;
//using Domain.Customers.Repositories;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Features.Orders.CancelOrder
//{
//    public class CancelOrderQueryCommand : IRequestHandler<CancelOrderCommand, Unit>
//    {
//        private readonly ICurrentUserService _userService;
//        private readonly ICustomerRepository _customerRepository;
//        private readonly IOrderRepository _orderRepository;

//        public CancelOrderQueryCommand(ICurrentUserService userService,
//                                       ICustomerRepository customerRepository,
//                                       IOrderRepository orderRepository)
//        {
//            _userService = userService;
//            _customerRepository = customerRepository;
//            _orderRepository = orderRepository;
//        }
//        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
//        {
//            var customerId = _userService.UserId;
//            var customer = await _customerRepository.GetCustomerById(customerId);

//            customer.CancelOrder(request.Id);

//            //await _orderRepository.Update();

//            return Unit.Value;
//        }
//    }
//}
