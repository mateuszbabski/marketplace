using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.ChangeProductAvailability
{
    public class ChangeProductAvailabilityCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
