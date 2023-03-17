using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Entrepreneurs.GetEntrepreneurDetails
{
    public class GetEntrepreneurDetailsQuery : IRequest<EntrepreneurDto>
    {
        public Guid Id { get; set; }
    }
}
