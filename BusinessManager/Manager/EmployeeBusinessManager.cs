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
        #endregion Private Properties

        #region Constructor
        public EmployeeBusinessManager(IEmployeeDataContract _employeDataManager)
        {
            employeDataManager = _employeDataManager;
        }
        #endregion Constructor

        #region Public Method

        public async Task<AddEditDeleteEmployeeResponse> CreateAsync(AddEmployeeRequest request)
        {
            AddEditDeleteEmployeeResponse response = new AddEditDeleteEmployeeResponse();
            if (!await request.IsValidAsync())
            {
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
                        response.IsSuccess = true;
                    }
                }
                catch (Exception ex)
                {
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
            GetEmployeesListResponse response = new GetEmployeesListResponse();
            if (!await request.IsValidAsync())
            {
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
                        response.IsSuccess = true;

                    }
                }
                catch (Exception ex)
                {
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
            AddEditDeleteEmployeeResponse response = new AddEditDeleteEmployeeResponse();
            if (!await request.IsValidAsync())
            {
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
                        response.IsSuccess = true;
                    }
                }
                catch (Exception ex)
                {
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
            AddEditDeleteEmployeeResponse response = new AddEditDeleteEmployeeResponse();
            if (!await request.IsValidAsync())
            {
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
                        response.IsSuccess = true;
                    }
                }
                catch (Exception ex)
                {
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
