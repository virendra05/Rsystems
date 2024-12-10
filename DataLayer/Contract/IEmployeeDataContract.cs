using DataManager.Model;

namespace DataManager.Contract
{
    public interface IEmployeeDataContract
    {
        Task<bool> CreateAsync(Employee employee);
        Task<EmployeeResult> GetEmployeesAsync(string searchText, string[] sortColumns, string sortOrder, int pageIndex, int pageSize);
        Task<bool> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
    }
}
