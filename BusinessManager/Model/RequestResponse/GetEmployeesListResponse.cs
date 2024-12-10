namespace BusinessManager.Model.RequestResponse
{
    public class GetEmployeesListResponse : BaseResponse
    {
        public List<GetEmployeeResponse> EmployeeList { get; set; }= new List<GetEmployeeResponse>();
        public int TotalPages { get; set; }
    }
}
