using BusinessManager.Model.Enums;
using BusinessManager.Model.Validator;
using FluentValidation.Results;

namespace BusinessManager.Model.RequestResponse
{
    public class BaseRequest
    {
        public List<ValidationFailure> ErrorList { get; } = new List<ValidationFailure>();
        public ErrorTypeEnum ErrorListCategory { get; set; }
        public virtual async Task<bool> IsValidAsync()
        {
            var validator = new BaseRequestValidator();
            var validationResult = await validator.ValidateAsync(this);
            if (!validationResult.IsValid)
            {
                ErrorListCategory = ErrorTypeEnum.Business_Validation;
                ErrorList.AddRange(validationResult.Errors);
                return false;
            }
            return true;
        }
    }
}
