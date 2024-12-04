namespace Employee.Api.ViewModel
{
    public class BaseResultViewModel
    {
        public List<string> Errors { get; set; } = new List<string>();
        public bool IsSuccess { get; set; }
    }
}
