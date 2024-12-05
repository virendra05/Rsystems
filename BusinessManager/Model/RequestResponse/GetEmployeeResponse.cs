using DM = DataManager.Model;

namespace BusinessManager.Model.RequestResponse
{
    public class GetEmployeeResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public static GetEmployeeResponse ConvertToBusiness(DM.Employee item)
        {
            GetEmployeeResponse Obj = new GetEmployeeResponse();
            Obj.Id = item.Id;
            Obj.FirstName = item.FirstName;
            Obj.LastName = item.LastName;
            Obj.Email = item.Email;
            Obj.PhoneNumber = item.PhoneNumber;          
            return Obj;

        }
    }
}
