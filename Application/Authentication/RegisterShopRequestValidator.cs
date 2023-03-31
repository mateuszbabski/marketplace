using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class RegisterShopRequestValidator : AbstractValidator<RegisterShopRequest>
    {
        public RegisterShopRequestValidator()
        {
            RuleFor(c => c.Email).NotEmpty()
                                 .EmailAddress()
                                 .WithMessage("Incorrect email format");

            RuleFor(c => c.Password).MinimumLength(6)
                                    .WithMessage("Password is too short");

            RuleFor(c => c.OwnerName).NotEmpty()
                                .WithMessage("Field cannot be empty");

            RuleFor(c => c.OwnerLastName).NotEmpty()
                                    .WithMessage("Field cannot be empty");

            RuleFor(c => c.ShopName).NotEmpty()
                                    .WithMessage("Field cannot be empty");

            RuleFor(c => c.TaxNumber).NotEmpty()
                                    .WithMessage("Field cannot be empty");

            RuleFor(c => c.Country).NotEmpty()
                                   .WithMessage("Field cannot be empty");

            RuleFor(c => c.City).NotEmpty()
                                .WithMessage("Field cannot be empty");

            RuleFor(c => c.Street).NotEmpty()
                                  .WithMessage("Field cannot be empty");

            RuleFor(c => c.PostalCode).NotEmpty()
                                      .WithMessage("Field cannot be empty");

            RuleFor(c => c.ContactNumber).NotEmpty()
                                           .Matches("^[0-9 +-]+$")
                                           .WithMessage("Only digits allowed");
        }

    }
}
