using FluentValidation;

namespace Application.Authentication
{
    public class RegisterCustomerRequestValidator : AbstractValidator<RegisterCustomerRequest>
    {
        public RegisterCustomerRequestValidator()
        {
            RuleFor(c => c.Email).NotEmpty()
                                 .EmailAddress()
                                 .WithMessage("Incorrect email format");

            RuleFor(c => c.Password).MinimumLength(6)
                                    .WithMessage("Password is too short");

            RuleFor(c => c.Name).NotEmpty()
                                .WithMessage("Field cannot be empty");

            RuleFor(c => c.LastName).NotEmpty()
                                    .WithMessage("Field cannot be empty");

            RuleFor(c => c.Country).NotEmpty()
                                   .WithMessage("Field cannot be empty");

            RuleFor(c => c.City).NotEmpty()
                                .WithMessage("Field cannot be empty");

            RuleFor(c => c.Street).NotEmpty()
                                  .WithMessage("Field cannot be empty");

            RuleFor(c => c.PostalCode).NotEmpty()
                                      .WithMessage("Field cannot be empty");

            RuleFor(c => c.TelephoneNumber).NotEmpty()
                                           .Matches("^[0-9]*$")
                                           .WithMessage("Only digits allowed");
        }
    }
}
