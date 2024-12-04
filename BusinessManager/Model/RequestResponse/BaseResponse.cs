using BusinessManager.Model.Enums;
using FluentValidation.Results;

namespace BusinessManager.Model.RequestResponse
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public IList<ValidationFailure> ErrorList { get; set; } = new List<ValidationFailure>();
        public ErrorTypeEnum ErrorListCategory { get; set; }
    }
}
