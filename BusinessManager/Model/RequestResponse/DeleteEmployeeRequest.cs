using BusinessManager.Model.Enums;
using BusinessManager.Model.Validator;

namespace BusinessManager.Model.RequestResponse
{
    public class DeleteEmployeeRequest : BaseRequest
    {
        public int Id { get; set; }
       

        public async override Task<bool> IsValidAsync()
        {
            bool IsValid = false;
            if (await base.IsValidAsync())
            {
                // Validations
                var validator = new DeleteEmployeeValidator();
                var validationResult = await validator.ValidateAsync(this);
                if (!validationResult.IsValid)
                {
                    ErrorListCategory = ErrorTypeEnum.Business_Validation;
                    ErrorList.AddRange(validationResult.Errors);
                }
                else
                {
                    IsValid = true;
                }
            }
            else
            {
                IsValid = false;
            }
            return IsValid;
        }

    }
}
