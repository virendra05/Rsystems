using BusinessManager.Model.RequestResponse;
using FluentValidation;

namespace BusinessManager.Model.Validator
{
    public class EditEmployeeValidator : AbstractValidator<EditEmployeeRequest>
    {
        public EditEmployeeValidator()
        {

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required and cannot be empty.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required and cannot be empty.");

            RuleFor(x => x.Email).EmailAddress().WithMessage("Please provide a valid email address.");

            RuleFor(x => x.PhoneNumber)
                .Length(10)
                .WithMessage("Phone number must be exactly 10 digits long.")
                .Must(phoneNumber => long.TryParse(phoneNumber, out _))
                .WithMessage("Phone number must contain only digits.");
        }
    }
}
