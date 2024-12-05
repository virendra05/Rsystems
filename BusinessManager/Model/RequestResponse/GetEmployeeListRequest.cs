namespace BusinessManager.Model.RequestResponse
{
    public class GetEmployeeListRequest:BaseRequest
    {
        public string SearchText { get; set; }
        public string[] SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
