using BusinessManager.Contract;
using BusinessManager.Model.Enums;
using BusinessManager.Model.RequestResponse;
using Employee.Api.CommonUtility;
using Employee.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Employee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Private Properties
        private IEmployeeBusinessContract employeeBusinessContract;
        private readonly ILoggingService _logger;
        #endregion Private Properties

        #region Constructor
        public EmployeeController(IEmployeeBusinessContract _employeeBusinessContract, ILoggingService logger)
        {
            employeeBusinessContract = _employeeBusinessContract;
            _logger = logger;
        }
        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Get All Employees
        /// </summary>
        /// <returns>Object of GetAllEmployeesResultViewModel</returns>
        [HttpGet]

        public async Task<IActionResult> Get(string? searchText = null, string sortColumn = "Email", int sortOrder = 1, int pageIndex = 1, int pageSize = 10)
        {
            _logger.LogInformation($"Starting Get method with SearchText: {searchText}, SortColumn: {sortColumn}, SortOrder: {sortOrder}, PageIndex: {pageIndex}, PageSize: {pageSize}");
            GetAllEmployeesResultViewModel result = new GetAllEmployeesResultViewModel();
            HttpStatusCode statusCode;
            try
            {
                var sortColumnsArray = sortColumn.Split(',');

                var data = await employeeBusinessContract.GetEmployeesAsync(new GetEmployeeListRequest()
                {
                    SearchText = searchText == null ? "" : searchText,
                    SortColumn = sortColumnsArray,
                    SortOrder = sortOrder == 1 ? "Asc" : "Desc",
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });
                _logger.LogInformation($"Data fetch attempt for Get method was successful: {data.IsSuccess}");
                if (data.IsSuccess)
                {
                    if (data.EmployeeList != null)
                    {
                        foreach (var item in data.EmployeeList)
                        {
                            GetEmployeeResultViewModel model = new GetEmployeeResultViewModel();
                            model.Id = item.Id;
                            model.FirstName = item.FirstName;
                            model.LastName = item.LastName;
                            model.Email = item.Email;
                            model.PhoneNumber = item.PhoneNumber;
                            result.EmployeeList.Add(model);

                        }
                    }
                    result.TotalPages = data.TotalPages;
                    result.TotalRecords = data.TotalRecords;
                    result.IsSuccess = true;
                }
                else
                {
                    List<string> err = new List<string>();
                    foreach (var item in data.ErrorList)
                    {
                        err.Add(item.ErrorMessage);
                    }
                    result.Errors = err;
                    result.IsSuccess = false;

                }
                statusCode = Utility.ParseBusinessStatusCodeIntoHttpStatusCode(result.IsSuccess,
                    data.ErrorListCategory, HttpVerbCode.GET);
                _logger.LogInformation($"Completed Get method with status code: {(int)statusCode}");

            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred during the Get operation.", ex);
                result.Errors.Add(Utility.GetDefaultInternalServerErrorException());
                statusCode = Utility.GetDefaultInternalServerErrorHttpStatusCode();
            }

            return StatusCode((int)statusCode, result);
        }

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <returns>Object of AddEditDeleteResultViewModel</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(EditEmployeeViewRequestModel model)
        {
            _logger.LogInformation($"Starting Put method with model ID: {model.Id}");

            AddEditDeleteResultViewModel result = new AddEditDeleteResultViewModel();
            HttpStatusCode statusCode;
            try
            {
                var data = await employeeBusinessContract.UpdateEmployeesAsync(new EditEmployeeRequest()
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber

                });
                _logger.LogInformation($"Update attempt was successful: {data.IsSuccess}");

                if (data.IsSuccess)
                {

                    result.IsSuccess = true;
                }
                else
                {
                    List<string> err = new List<string>();
                    foreach (var item in data.ErrorList)
                    {
                        err.Add(item.ErrorMessage);
                    }
                    result.Errors = err;
                    result.IsSuccess = false;

                }
                statusCode = Utility.ParseBusinessStatusCodeIntoHttpStatusCode(result.IsSuccess,
                    data.ErrorListCategory, HttpVerbCode.PUT);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred during the Put operation", ex);
                result.Errors.Add(Utility.GetDefaultInternalServerErrorException());
                statusCode = Utility.GetDefaultInternalServerErrorHttpStatusCode();
            }

            return StatusCode((int)statusCode, result);
        }

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <returns>Object of AddEditDeleteResultViewModel</returns>
        [HttpPost]
        public async Task<IActionResult> Post(AddEmployeeViewRequestModel model)
        {
            _logger.LogInformation($"Starting Post method with model: {model}");

            AddEditDeleteResultViewModel result = new AddEditDeleteResultViewModel();
            HttpStatusCode statusCode;
            try
            {
                var data = await employeeBusinessContract.CreateAsync(new AddEmployeeRequest()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber

                });
                _logger.LogInformation($"Creation attempt was successful: {data.IsSuccess}");
                if (data.IsSuccess)
                {

                    result.IsSuccess = true;
                }
                else
                {
                    List<string> err = new List<string>();
                    foreach (var item in data.ErrorList)
                    {
                        err.Add(item.ErrorMessage);
                    }
                    result.Errors = err;
                    result.IsSuccess = false;

                }
                statusCode = Utility.ParseBusinessStatusCodeIntoHttpStatusCode(result.IsSuccess,
                    data.ErrorListCategory, HttpVerbCode.POST);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred during the Post operation", ex);
                result.Errors.Add(Utility.GetDefaultInternalServerErrorException());
                statusCode = Utility.GetDefaultInternalServerErrorHttpStatusCode();
            }

            return StatusCode((int)statusCode, result);
        }

        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <returns>Object of AddEditDeleteResultViewModel</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Starting Delete method with Employee ID: {id}");
            AddEditDeleteResultViewModel result = new AddEditDeleteResultViewModel();
            HttpStatusCode statusCode;
            try
            {
                var data = await employeeBusinessContract.DeleteEmployeesAsync(new DeleteEmployeeRequest()
                {
                    Id = id,
                });
                _logger.LogInformation($"Delete attempt was successful: {data.IsSuccess}");
                if (data.IsSuccess)
                {

                    result.IsSuccess = true;
                }
                else
                {
                    List<string> err = new List<string>();
                    foreach (var item in data.ErrorList)
                    {
                        err.Add(item.ErrorMessage);
                    }
                    result.Errors = err;
                    result.IsSuccess = false;

                }
                statusCode = Utility.ParseBusinessStatusCodeIntoHttpStatusCode(result.IsSuccess,
                    data.ErrorListCategory, HttpVerbCode.DELETE);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred during the Delete operation", ex);
                result.Errors.Add(Utility.GetDefaultInternalServerErrorException());
                statusCode = Utility.GetDefaultInternalServerErrorHttpStatusCode();
            }

            return StatusCode((int)statusCode, result);
        }
        #endregion Public Methods


    }
}