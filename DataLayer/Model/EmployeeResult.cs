using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Model
{
    public class EmployeeResult
    {
        public List<Employee> Employees { get; set; }= new List<Employee>();
        public int TotalPages { get; set; }
    }
}
