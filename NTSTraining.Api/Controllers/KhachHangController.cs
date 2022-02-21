using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTS.Common.Models;
using NTSTraining.Services.KhachHang;
using System.Threading.Tasks;
using ToolManage.Api.Controllers;
using NTSTraining.Models.Models.KhachHang;
using NTSCommon.Models;

namespace NTSTraining.Api.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class KhachHangController : BaseApiController
    {
        public readonly IKhachHangService ikhachHangService;
        public KhachHangController(IKhachHangService _ikhachHangService)
        {
            ikhachHangService = _ikhachHangService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<ApiResultModel>> createKhachHang([FromBody] KhachHangCreateModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };
            await ikhachHangService.CreateKhachHangAsync(model);

            return Ok(apiResultModel);
        }
        [HttpPost]
        [Route("search")]
        public async Task<ActionResult<SearchBaseResultModel<KhachHangSearchModel>>> SearchLoaiKhachHang([FromBody] KhachHangSearchModel modelSearch)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await ikhachHangService.SearchKhachHangAsync(modelSearch);

            return Ok(apiResultModel);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ActionResult<KhachHangCreateModel>>> GetKhachHangById([FromRoute] string id)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };
            apiResultModel.Data = await ikhachHangService.GetKhachHangById(id);

            return Ok(apiResultModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ApiResultModel>> DeleteLoaiKhachHang([FromRoute] string id)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };
            await ikhachHangService.DeleteKhachHangByIdAsync(id);
            return Ok(apiResultModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ApiResultModel>> UpdateLoaiKhachHang([FromRoute] string id, [FromBody] KhachHangCreateModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await ikhachHangService.UpdateKhachHangAsync(id, model);

            return Ok(apiResultModel);
        }
    }
}
