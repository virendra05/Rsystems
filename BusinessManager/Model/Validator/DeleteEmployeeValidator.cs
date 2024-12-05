using BusinessManager.Model.RequestResponse;
using FluentValidation;

namespace BusinessManager.Model.Validator
{
    public class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeRequest>
    {
        public DeleteEmployeeValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("The provided ID is invalid. ID must be greater than zero.");

        }
    }
}
