namespace Employee.Api.ViewModel
{
    public class GetAllEmployeesResultViewModel:BaseResultViewModel
    {
        public List<GetEmployeeResultViewModel> EmployeeList { get; set; } = new List<GetEmployeeResultViewModel>();
    }
}
