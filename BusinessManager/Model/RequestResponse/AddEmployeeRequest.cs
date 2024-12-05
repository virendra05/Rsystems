using BusinessManager.Model.Enums;
using BusinessManager.Model.Validator;
using DM = DataManager.Model;

namespace BusinessManager.Model.RequestResponse
{
    public class AddEmployeeRequest: BaseRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public async override Task<bool> IsValidAsync()
        {
            bool IsValid = false;
            if (await base.IsValidAsync())
            {
                // Validations
                var validator = new AddEmployeeValidator();
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

        public static DM.Employee ConvertToData(AddEmployeeRequest employee)
        {
            var EmployeeDM = new DM.Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber

            };
            return EmployeeDM;
        }

    }
}
