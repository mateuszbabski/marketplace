using Domain.Customers;
using Domain.Entrepreneur.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Entrepreneurs.GetEntrepreneurDetails
{
    public class GetEntrepreneurDetailsQueryHandler : IRequestHandler<GetEntrepreneurDetailsQuery, EntrepreneurDto>
    {
        private readonly IEntrepreneurRepository _entrepreneurRepository;

        public GetEntrepreneurDetailsQueryHandler(IEntrepreneurRepository entrepreneurRepository)
        {
            _entrepreneurRepository = entrepreneurRepository;
        }
        public async Task<EntrepreneurDto> Handle(GetEntrepreneurDetailsQuery request, CancellationToken cancellationToken)
        {
            var entrepreneur = await _entrepreneurRepository.GetEntrepreneurById(request.Id);

            if (entrepreneur == null)
            {
                throw new Exception("Entrepreneur not found.");
            }

            var entrepreneurViewModel = new EntrepreneurDto()
            {
                Id = entrepreneur.Id,
                Email = entrepreneur.Email,
                Name = entrepreneur.Name,
                LastName = entrepreneur.LastName,
                ShopName = entrepreneur.ShopName,
                TaxNumber = entrepreneur.TaxNumber,
                TelephoneNumber = entrepreneur.TelephoneNumber,
                Country = entrepreneur.ShopAddress.Country,
                City = entrepreneur.ShopAddress.City,
                PostalCode = entrepreneur.ShopAddress.PostalCode,
                Street = entrepreneur.ShopAddress.Street
            };

            return entrepreneurViewModel;
        }
    }
}
