using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NTS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTS.Common.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<ErrorValidateModel> errors = new List<ErrorValidateModel>();
                foreach (var item in context.ModelState.ToList())
                {
                    foreach (var error in item.Value.Errors)
                    {
                        errors.Add(new ErrorValidateModel
                        {
                            Message = error.ErrorMessage
                        });
                    }
                }

                var errorModel = new ApiResultModel<List<ErrorValidateModel>>()
                {
                    Data = errors,
                    StatusCode = ApiResultConstants.StatusCodeValidateError
                };

                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented
                };

                var contentResult = JsonConvert.SerializeObject(errorModel, serializerSettings);

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                context.HttpContext.Response.WriteAsync(contentResult);

                context.Result = new OkResult();
            }
        }
    }
}
