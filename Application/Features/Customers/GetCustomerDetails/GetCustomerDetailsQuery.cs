using MediatR;

namespace Application.Features.Customers.GetCustomerDetails
{
    public class GetCustomerDetailsQuery : IRequest<CustomerDto>
    {
        public Guid Id { get; set; }
    }
}
