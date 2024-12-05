using BusinessManager.Model.RequestResponse;

namespace BusinessManager.Contract
{
    public interface IEmployeeBusinessContract
    {
        Task<AddEditDeleteEmployeeResponse> CreateAsync(AddEmployeeRequest request);
        Task<GetEmployeesListResponse> GetEmployeesAsync(GetEmployeeListRequest request);
        Task<AddEditDeleteEmployeeResponse> UpdateEmployeesAsync(EditEmployeeRequest request);
        Task<AddEditDeleteEmployeeResponse> DeleteEmployeesAsync(DeleteEmployeeRequest request);

    }
}
