using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.AddProduct
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(c => c.ProductName).NotEmpty()
                                       .WithMessage("Password is too short");

            RuleFor(c => c.ProductDescription).NotEmpty()
                                              .WithMessage("Field cannot be empty");

            RuleFor(c => c.Amount).NotEmpty()
                                        .WithMessage("Field cannot be empty");

            RuleFor(c => c.Currency).NotEmpty()
                                        .WithMessage("Field cannot be empty");

            RuleFor(c => c.Unit).NotEmpty()
                                .WithMessage("Field cannot be empty");
        }
    }
}
