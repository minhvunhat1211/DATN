using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Results;

namespace VnEdu.Api.Filters
{
    /// <summary>
    /// Information of VnEduExceptionAttibute
    /// CreatedBy: MinhVN(24/12/2022)
    /// </summary>
    public class VnEduExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// OnException
        /// </summary>
        /// <param name="context">ExceptionContext</param>
        /// CreatedBy: MinhVN(24/12/2022)
        public override void OnException(ExceptionContext context)
        {
            var apiError = new OperationResult<bool>();
            apiError.Data = false;
            apiError.AddError(ErrorCode.ServerError, $"{context.Exception.Message}");

            context.Result = new JsonResult(apiError) { StatusCode = 500 };
        }
    }
}