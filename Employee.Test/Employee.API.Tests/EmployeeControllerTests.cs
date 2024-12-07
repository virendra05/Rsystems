using BusinessManager.Contract;
using Employee.Api.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Test.Employee.API.Tests
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeBusinessContract> _employeeBusinessContractMock;
        private readonly Mock<ILoggingService> _loggerMock;
        private readonly EmployeeController _employeeController;

        public EmployeeControllerTests()
        {
            _loggerMock = new Mock<ILoggingService>();
            _employeeBusinessContractMock = new Mock<IEmployeeBusinessContract>();
            _employeeController = new EmployeeController(_employeeBusinessContractMock.Object, _loggerMock.Object);
        }
    }
}
