using BusinessManager.Model.RequestResponse;
using FluentValidation;

namespace BusinessManager.Model.Validator
{
    public class AddEmployeeValidator:AbstractValidator<AddEmployeeRequest>
    {
        public AddEmployeeValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required and cannot be empty.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required and cannot be empty.");

            RuleFor(x => x.Email).EmailAddress().WithMessage("Please provide a valid email address.");
        }

    }
}
