using BusinessManager.Model.Enums;
using System.Net;

namespace Employee.Api.CommonUtility
{
    public class Utility
    {
        public static HttpStatusCode ParseBusinessStatusCodeIntoHttpStatusCode(bool IsSuccess, ErrorTypeEnum errorType, HttpVerbCode httpVerbCode)
        {
            HttpStatusCode statusCode;
            if (IsSuccess)
            {
                switch (httpVerbCode)
                {
                    case HttpVerbCode.POST:
                        statusCode = HttpStatusCode.Created;
                        break;
                    case HttpVerbCode.GET:
                        statusCode = HttpStatusCode.OK;
                        break;
                    case HttpVerbCode.PUT:
                        statusCode = HttpStatusCode.OK;
                        break;
                    case HttpVerbCode.DELETE:
                        statusCode = HttpStatusCode.OK;
                        break;
                    default:
                        statusCode = HttpStatusCode.OK;
                        break;
                }
            }
            else
            {
                switch (errorType)
                {
                    case ErrorTypeEnum.Unknown:
                        statusCode = HttpStatusCode.InternalServerError;
                        break;
                    case ErrorTypeEnum.Data_Validation:
                        statusCode = HttpStatusCode.UnprocessableEntity;
                        break;
                    case ErrorTypeEnum.Business_Validation:
                        statusCode = HttpStatusCode.UnprocessableEntity;
                        break;
                    case ErrorTypeEnum.Internal_Server_Error:
                        statusCode = HttpStatusCode.InternalServerError;
                        break;
                    case ErrorTypeEnum.Data_Not_Found:
                        statusCode = HttpStatusCode.NotFound;
                        break;
                    case ErrorTypeEnum.NotModified:
                        statusCode = HttpStatusCode.NotModified;
                        break;
                    case ErrorTypeEnum.Duplicate_Data_Found:
                        statusCode = HttpStatusCode.UnprocessableEntity;
                        break;
                    default:
                        statusCode = HttpStatusCode.InternalServerError;
                        break;
                }
            }
            return statusCode;
        }

        public static string GetDefaultInternalServerErrorException()
        {
            return "Internal Server Error!";
        }

        public static HttpStatusCode GetDefaultInternalServerErrorHttpStatusCode()
        {
            return HttpStatusCode.InternalServerError;
        }
    }
}
