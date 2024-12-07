using DataManager.Contract;
using DataManager.Model;
using System.Text.Json;

namespace DataManager.Manager
{
    public class EmployeeDataManager : IEmployeeDataContract
    {
        private readonly string _dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        private readonly string _filePath;

        public EmployeeDataManager()
        {
            if (!Directory.Exists(_dataDirectory))
            {
                Directory.CreateDirectory(_dataDirectory);
            }
            _filePath = Path.Combine(_dataDirectory, "Employees.json");
        }

        #region public methods
        public async Task<bool> CreateAsync(Employee employee)
        {
            var employees = await ReadEmployeesAsync();
            employee.Id = employees.Any() ? employees.Max(e => e.Id) + 1 : 1; // Assign ID, auto-increment
            employees.Add(employee);
            await WriteEmployeesAsync(employees);
            return true;
        }

        public async Task<List<Employee>> GetEmployeesAsync(string searchText, string[] sortColumns, string sortOrder, int pageIndex, int pageSize)
        {
            var employees = await ReadEmployeesAsync();
            var query = employees.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(e => e.FirstName.Contains(searchText) || e.LastName.Contains(searchText) || e.Email.Contains(searchText));
            }

            if (sortColumns != null && sortColumns.Length > 0)
            {
                foreach (var column in sortColumns)
                {
                    switch (column.ToLower())
                    {
                        case "firstname":
                            query = sortOrder == "ASC" ? query.OrderBy(e => e.FirstName) : query.OrderByDescending(e => e.FirstName);
                            break;
                        case "lastname":
                            query = sortOrder == "ASC" ? query.OrderBy(e => e.LastName) : query.OrderByDescending(e => e.LastName);
                            break;
                        case "email":
                            query = sortOrder == "ASC" ? query.OrderBy(e => e.Email) : query.OrderByDescending(e => e.Email);
                            break;
                        default:
                            break;
                    }
                }
            }

            var paginatedList = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return paginatedList.Select(e => new Employee
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber
            }).ToList();
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            var employees = await ReadEmployeesAsync();
            var index = employees.FindIndex(e => e.Id == employee.Id);
            if (index != -1)
            {
                employees[index] = employee;
                await WriteEmployeesAsync(employees);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employees = await ReadEmployeesAsync();
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                employees.Remove(employee);
                await WriteEmployeesAsync(employees);
                return true;
            }
            return false;
        }

        #endregion public methods

        #region private methods
        private async Task<List<Employee>> ReadEmployeesAsync()
        {
            try
            {
                using (FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    return await JsonSerializer.DeserializeAsync<List<Employee>>(fs) ?? new List<Employee>();
                }
            }
            catch
            {
                return new List<Employee>();
            }
        }

        private async Task WriteEmployeesAsync(List<Employee> employees)
        {
            using (FileStream fs = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
            {
                await JsonSerializer.SerializeAsync(fs, employees);
            }
        }
        #endregion private methods
    }
}
