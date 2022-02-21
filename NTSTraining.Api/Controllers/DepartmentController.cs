using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTS.Common.Models;
using NTSCommon.Models;
using NTSTraining.Models.Models.Department;
using NTSTraining.Services.Department;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolManage.Api.Controllers;

namespace NTSTraining.Api.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : BaseApiController
    {
        public readonly IDepartmentService iDepartmentService;

        public DepartmentController(IDepartmentService _iDepartmentService)
        {
            iDepartmentService = _iDepartmentService;
        }

        [HttpPost]
        [Route("search")]
        public async Task<ActionResult<SearchBaseResultModel<DepartmentSearchModel>>> SearchLoaiKhachHang([FromBody] DepartmentSearchModel modelSearch)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await iDepartmentService.searchDepartment(modelSearch);

            return Ok(apiResultModel);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ActionResult<DepartmentSearchModel>>> GetLoaiKhachHangById([FromRoute] string id)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };
            apiResultModel.Data = await iDepartmentService.getDepartmentById(id);

            return Ok(apiResultModel);
        }


        [HttpPost]
        public async Task<ActionResult<ApiResultModel>> CreateLoaiKhachHang([FromBody] DepartmentModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };
            await iDepartmentService.createDepartment(model);

            return Ok(apiResultModel);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ApiResultModel>> UpdateLoaiKhachHang([FromRoute] string id, [FromBody] DepartmentModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await iDepartmentService.updateDepartment(id, model);

            return Ok(apiResultModel);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ApiResultModel>> DeleteDepartment([FromRoute] string id)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };
            await iDepartmentService.deleteDepartment(id);
            return Ok(apiResultModel);
        }
        //get all loại khách hàng
        [HttpGet]
        public async Task<ActionResult<List<DepartmentSearchModel>>> GetAllDepatment()
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await iDepartmentService.getAllDepartment();

            return Ok(apiResultModel);
        }

    }
}
