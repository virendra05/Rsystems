using BusinessManager.Model.RequestResponse;
using FluentValidation;

namespace BusinessManager.Model.Validator
{
    public class BaseRequestValidator : AbstractValidator<BaseRequest>
    {
        public BaseRequestValidator()
        {

        }
    }
}
