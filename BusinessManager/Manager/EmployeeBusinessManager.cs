using BusinessManager.Contract;
using BusinessManager.Model.Enums;
using BusinessManager.Model.ErrorMessage;
using BusinessManager.Model.RequestResponse;
using DataManager.Contract;
using FluentValidation.Results;

namespace BusinessManager.Manager
{
    public class EmployeeBusinessManager : IEmployeeBusinessContract
    {
        #region Private Properties
        private IEmployeeDataContract employeDataManager;
        private readonly ILoggingService _logger;
        #endregion Private Properties

        #region Constructor
        public EmployeeBusinessManager(IEmployeeDataContract _employeDataManager, ILoggingService logger)
        {
            _logger = logger;
            employeDataManager = _employeDataManager;
        }
        #endregion Constructor

        #region Public Method

        public async Task<AddEditDeleteEmployeeResponse> CreateAsync(AddEmployeeRequest request)
        {
            _logger.LogInformation($"Starting CreateAsync with request: {request}");
            AddEditDeleteEmployeeResponse response = new AddEditDeleteEmployeeResponse();
            if (!await request.IsValidAsync())
            {
                _logger.LogWarning($"Validation failed for CreateAsync with errors: {request.ErrorList}");
                response.IsSuccess = false;
                response.ErrorListCategory = ErrorTypeEnum.Business_Validation;
                response.ErrorList = request.ErrorList;
            }
            else
            {
                try
                {
                    var employeeDataModel = AddEmployeeRequest.ConvertToData(request);
                    var isAdded = await employeDataManager.CreateAsync(employeeDataModel);
                    if (isAdded == true)
                    {
                        _logger.LogInformation($"Employee created successfully with ID: {employeeDataModel.Id}");
                        response.IsSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occurred in CreateAsync.", ex);
                    response.IsSuccess = false;
                    response.ErrorListCategory = ErrorTypeEnum.Internal_Server_Error;
                    var Error = new ValidationFailure(CommonErrorMessageForLogging.Error_Key, CommonErrorMessageForLogging.Internal_Server_Error);
                    response.ErrorList.Add(Error);
                }
            }
            return response;
        }

        public async Task<GetEmployeesListResponse> GetEmployeesAsync(GetEmployeeListRequest request)
        {
            _logger.LogInformation($"Starting GetEmployeesAsync with request: {request}");
            GetEmployeesListResponse response = new GetEmployeesListResponse();
            if (!await request.IsValidAsync())
            {
                _logger.LogWarning($"Validation failed for GetEmployeesAsync with errors: {request.ErrorList}");
                response.IsSuccess = false;
                response.ErrorListCategory = ErrorTypeEnum.Business_Validation;
                response.ErrorList = request.ErrorList;
            }
            else
            {
                try
                {
                    var data = await employeDataManager.GetEmployeesAsync(request.SearchText, request.SortColumn, request.SortOrder, request.PageIndex, request.PageSize);

                    if (data == null || data.Count == 0)
                    {
                        _logger.LogInformation("No data found in GetEmployeesAsync.");
                        response.IsSuccess = true;
                        response.ErrorListCategory = ErrorTypeEnum.Data_Not_Found;
                        return response;
                    }
                    else
                    {
                        foreach (var item in data)
                        {
                            var addItem = GetEmployeeResponse.ConvertToBusiness(item);
                            response.EmployeeList.Add(addItem);
                        }
                        _logger.LogInformation("Data retrieved successfully in GetEmployeesAsync.");
                        response.IsSuccess = true;

                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occurred in GetEmployeesAsync.", ex);
                    response.IsSuccess = false;
                    response.ErrorListCategory = ErrorTypeEnum.Internal_Server_Error;
                    var Error = new ValidationFailure(CommonErrorMessageForLogging.Error_Key, CommonErrorMessageForLogging.Internal_Server_Error);
                    response.ErrorList.Add(Error);
                }
            }
            return response;
        }

        public async Task<AddEditDeleteEmployeeResponse> UpdateEmployeesAsync(EditEmployeeRequest request)
        {
            _logger.LogInformation($"Starting UpdateEmployeesAsync with request: {request}");
            AddEditDeleteEmployeeResponse response = new AddEditDeleteEmployeeResponse();
            if (!await request.IsValidAsync())
            {
                _logger.LogWarning($"Validation failed for UpdateEmployeesAsync with errors: {request.ErrorList}");
                response.IsSuccess = false;
                response.ErrorListCategory = ErrorTypeEnum.Business_Validation;
                response.ErrorList = request.ErrorList;
            }
            else
            {
                try
                {
                    var employeeDataModel = EditEmployeeRequest.ConvertToData(request);
                    var isAdded = await employeDataManager.UpdateAsync(employeeDataModel);
                    if (isAdded == true)
                    {
                        _logger.LogInformation($"Employee updated successfully with ID: {employeeDataModel.Id}");
                        response.IsSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occurred in UpdateEmployeesAsync.", ex);
                    response.IsSuccess = false;
                    response.ErrorListCategory = ErrorTypeEnum.Internal_Server_Error;
                    var Error = new ValidationFailure(CommonErrorMessageForLogging.Error_Key, CommonErrorMessageForLogging.Internal_Server_Error);
                    response.ErrorList.Add(Error);
                }
            }
            return response;
        }

        public async Task<AddEditDeleteEmployeeResponse> DeleteEmployeesAsync(DeleteEmployeeRequest request)
        {
            _logger.LogInformation($"Starting DeleteEmployeesAsync with request: {request}");
            AddEditDeleteEmployeeResponse response = new AddEditDeleteEmployeeResponse();
            if (!await request.IsValidAsync())
            {
                _logger.LogWarning($"Validation failed for DeleteEmployeesAsync with errors: {request.ErrorList}");
                response.IsSuccess = false;
                response.ErrorListCategory = ErrorTypeEnum.Business_Validation;
                response.ErrorList = request.ErrorList;
            }
            else
            {
                try
                {

                    var isDeleted = await employeDataManager.DeleteAsync(request.Id);
                    if (isDeleted == true)
                    {
                        _logger.LogInformation($"Employee deleted successfully with ID: {request.Id}");
                        response.IsSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occurred in DeleteEmployeesAsync.", ex);
                    response.IsSuccess = false;
                    response.ErrorListCategory = ErrorTypeEnum.Internal_Server_Error;
                    var Error = new ValidationFailure(CommonErrorMessageForLogging.Error_Key, CommonErrorMessageForLogging.Internal_Server_Error);
                    response.ErrorList.Add(Error);
                }
            }
            return response;
        }

        #endregion Public Method
    }
}
