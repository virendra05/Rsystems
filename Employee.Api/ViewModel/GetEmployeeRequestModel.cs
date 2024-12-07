namespace Employee.Api.ViewModel
{
    public class GetEmployeeRequestModel
    {
        public string SearchText { get; set; }
        public string SortColumn { get; set; }
        public int SortOrder { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
