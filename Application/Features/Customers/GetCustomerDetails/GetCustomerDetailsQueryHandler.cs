using Domain.Customers.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.GetCustomerDetails
{
    public class GetCustomerDetailsQueryHandler : IRequestHandler<GetCustomerDetailsQuery, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerDetailsQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerById(request.Id);

            if(customer == null)
            {
                throw new Exception("Customer not found");
            }

            var customerViewModel = new CustomerDto()
            {
                Id = customer.Id,
                Email = customer.Email,
                Name = customer.Name,
                LastName = customer.LastName,
                TelephoneNumber = customer.TelephoneNumber,
                Country = customer.Address.Country,
                City = customer.Address.City,
                PostalCode = customer.Address.PostalCode,
                Street = customer.Address.Street
            };

            return customerViewModel;
        }
    }
}
