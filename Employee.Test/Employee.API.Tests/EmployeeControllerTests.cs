using BusinessManager.Contract;
using BusinessManager.Model.RequestResponse;
using Employee.Api.Controllers;
using Employee.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;


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

        [Fact]
        public async Task Get_Returns_AllEmployees_ReturnsSuccessResult()
        {
            var mockResponse = new GetEmployeesListResponse
            {
                IsSuccess = true,
                EmployeeList = new List<GetEmployeeResponse>
                {
                    new GetEmployeeResponse { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com"},
                    new GetEmployeeResponse { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com"}
                },
                TotalPages = 1,
                TotalRecords = 2,
            };

            _employeeBusinessContractMock.Setup(x => x.GetEmployeesAsync(It.IsAny<GetEmployeeListRequest>()))
                .ReturnsAsync(mockResponse);

            var result = await _employeeController.Get() as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode); // Updated status code check
            var responseModel = Assert.IsType<GetAllEmployeesResultViewModel>(result.Value);
            Assert.True(responseModel.IsSuccess);
            Assert.Equal(2, responseModel.EmployeeList.Count);
            Assert.Equal("John", responseModel.EmployeeList[0].FirstName);
            Assert.Equal("Jane", responseModel.EmployeeList[1].FirstName);
            _loggerMock.Verify(x => x.LogInformation(It.IsAny<string>()), Times.AtLeastOnce());
        }

        [Fact]
        public async Task Get_Returns_AllEmployees_ReturnsInternalServerError()
        {
            _employeeBusinessContractMock.Setup(cb => cb.GetEmployeesAsync(It.IsAny<GetEmployeeListRequest>())).ThrowsAsync(new Exception("Database error"));

            var result = await _employeeController.Get() as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
            var responseModel = Assert.IsType<GetAllEmployeesResultViewModel>(result.Value);
            Assert.False(responseModel.IsSuccess);
        }

        [Fact]
        public async Task AddEmployee_Success_ReturnsSuccessResult()
        {
            var viewModel = new AddEmployeeViewRequestModel
            {
                FirstName = "Virendra",
                LastName = "Parade",
                Email = "virendraparade01@gmail.com"
            };

            var mockResponse = new AddEditDeleteEmployeeResponse { IsSuccess = true};

            _employeeBusinessContractMock.Setup(x => x.CreateAsync(It.IsAny<AddEmployeeRequest>()))
               .ReturnsAsync(mockResponse);

            var result = await _employeeController.Post(viewModel) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
            var responseModel = Assert.IsType<AddEditDeleteResultViewModel>(result.Value);
            Assert.True(responseModel.IsSuccess);
        }

        [Fact]
        public async Task AddEmployee_Failure_ReturnsBadRequest()
        {
            var viewModel = new AddEmployeeViewRequestModel
            {
                FirstName = "Virendra",
                LastName = "Parade",
                Email = "virendraparade01@gmail.com"
            };

            var mockResponse = new AddEditDeleteEmployeeResponse
            {
                IsSuccess = false,
            };

            _employeeBusinessContractMock.Setup(x => x.CreateAsync(It.IsAny<AddEmployeeRequest>()))
            .ReturnsAsync(mockResponse);

            var result = await _employeeController.Post(viewModel) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
            var responseModel = Assert.IsType<AddEditDeleteResultViewModel>(result.Value);
            Assert.False(responseModel.IsSuccess);
        }

        [Fact]
        public async Task DeleteEmployee_Success_ReturnsSuccessResult()
        {
            var mockResponse = new AddEditDeleteEmployeeResponse { IsSuccess = true };

            _employeeBusinessContractMock.Setup(x => x.DeleteEmployeesAsync(It.IsAny<DeleteEmployeeRequest>()))
               .ReturnsAsync(mockResponse);

            var result = await _employeeController.DeleteAsync(1) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            var responseModel = Assert.IsType<AddEditDeleteResultViewModel>(result.Value);
            Assert.True(responseModel.IsSuccess);
        }

        [Fact]
        public async Task DeleteEmployee_Failure_ReturnsBadRequest()
        {
           
            var mockResponse = new AddEditDeleteEmployeeResponse
            {
                IsSuccess = false,
            };

            _employeeBusinessContractMock.Setup(x => x.DeleteEmployeesAsync(It.IsAny<DeleteEmployeeRequest>()))
            .ReturnsAsync(mockResponse);

            var result = await _employeeController.DeleteAsync(2) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
            var responseModel = Assert.IsType<AddEditDeleteResultViewModel>(result.Value);
            Assert.False(responseModel.IsSuccess);
        }

        [Fact]
        public async Task UpdateEmployee_Success_ReturnsSuccessResult()
        {
            var viewModel = new EditEmployeeViewRequestModel
            {
                Id = 1,
                FirstName = "Virendra",
                LastName = "Parade",
                Email = "virendraparade01@gmail.com"
            };

            var mockResponse = new AddEditDeleteEmployeeResponse { IsSuccess = true };

            _employeeBusinessContractMock.Setup(x => x.UpdateEmployeesAsync(It.IsAny<EditEmployeeRequest>()))
               .ReturnsAsync(mockResponse);

            var result = await _employeeController.UpdateAsync(viewModel) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            var responseModel = Assert.IsType<AddEditDeleteResultViewModel>(result.Value);
            Assert.True(responseModel.IsSuccess);
        }

        [Fact]
        public async Task UpdateEmployee_Failure_ReturnsBadRequest()
        {
            var viewModel = new EditEmployeeViewRequestModel
            {
                Id = 1,
                FirstName = "Virendra",
                LastName = "Parade",
                Email = "virendraparade01@gmail.com"
            };

            var mockResponse = new AddEditDeleteEmployeeResponse
            {
                IsSuccess = false,
            };

            _employeeBusinessContractMock.Setup(x => x.UpdateEmployeesAsync(It.IsAny<EditEmployeeRequest>()))
            .ReturnsAsync(mockResponse);

            var result = await _employeeController.UpdateAsync(viewModel) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
            var responseModel = Assert.IsType<AddEditDeleteResultViewModel>(result.Value);
            Assert.False(responseModel.IsSuccess);
        }
    }
}
