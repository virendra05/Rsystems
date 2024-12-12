using BusinessManager.Model.Enums;
using BusinessManager.Model.Validator;
using DM = DataManager.Model;

namespace BusinessManager.Model.RequestResponse
{
    public class EditEmployeeRequest : BaseRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public async override Task<bool> IsValidAsync()
        {
            bool IsValid = false;
            if (await base.IsValidAsync())
            {
                var validator = new EditEmployeeValidator();
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

        public static DM.Employee ConvertToData(EditEmployeeRequest employee)
        {
            var EmployeeDM = new DM.Employee()
            {
                Id=employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email

            };
            return EmployeeDM;
        }

    }
}
