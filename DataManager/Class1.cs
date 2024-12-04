using DataContract;
using DataLayer;
using System.Data.SqlClient;

namespace DataManager
{
    public class EmployeeRepository : IEmployeeDataContract
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Employee employee)
        {
            var name = new SqlParameter("@Name", employee.Name ?? (object)DBNull.Value);
            var email = new SqlParameter("@Email", employee.Email ?? (object)DBNull.Value);
            var phoneNumber = new SqlParameter("@PhoneNumber", employee.PhoneNumber ?? (object)DBNull.Value);
            var designationId = new SqlParameter("@DesignationId", employee.DesignationId);
            var teamName = new SqlParameter("@TeamName", employee.TeamName ?? (object)DBNull.Value);
            var numberOfEmployees = new SqlParameter("@NumberOfEmployees", employee.NumberOfEmployees);
            var unitName = new SqlParameter("@UnitName", employee.UnitName ?? (object)DBNull.Value);
            var budgetAmount = new SqlParameter("@BudgetAmount", employee.BudgetAmount);

            await _context.Database.ExecuteSqlRawAsync("EXEC SaveEmployee @Name, @Email, @PhoneNumber, @DesignationId, @TeamName, @NumberOfEmployees, @UnitName, @BudgetAmount",
                name, email, phoneNumber, designationId, teamName, numberOfEmployees, unitName, budgetAmount);
        }
    }

}