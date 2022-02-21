using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTS.Common.Models;
using NTSCommon.Models;
using NTSTraining.Models.Models.Employee;
using NTSTraining.Services.Employee;
using System.Threading.Tasks;
using ToolManage.Api.Controllers;

namespace NTSTraining.Api.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : BaseApiController
    {
        public readonly IEmployeeService iEmployeeService;

        public EmployeeController(IEmployeeService _iEmployeeService)
        {
            iEmployeeService = _iEmployeeService;
        }

        [HttpPost]
        [Route("search")]
        public async Task<ActionResult<SearchBaseResultModel<EmployeeSearchModel>>> searchEmployee([FromBody] EmployeeSearchModel modelSearch)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await iEmployeeService.searchEmployee(modelSearch);

            return Ok(apiResultModel);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ActionResult<EmployeeSearchModel>>> getEmployeeById([FromRoute] string id)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };
            apiResultModel.Data = await iEmployeeService.getEmployeeById(id);

            return Ok(apiResultModel);
        }


        [HttpPost]
        public async Task<ActionResult<ApiResultModel>> createEmployee([FromBody] EmployeeModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };
            await iEmployeeService.createEmployee(model);

            return Ok(apiResultModel);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ApiResultModel>> updateEmployee([FromRoute] string id, [FromBody] EmployeeModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await iEmployeeService.updateEmployee(id, model);

            return Ok(apiResultModel);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ApiResultModel>> deleteEmployee([FromRoute] string id)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };
            await iEmployeeService.deleteEmployee(id);
            return Ok(apiResultModel);
        }
    }
}
