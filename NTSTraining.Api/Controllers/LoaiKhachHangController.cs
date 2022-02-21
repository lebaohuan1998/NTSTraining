using Microsoft.AspNetCore.Mvc;
using NTS.Common.Models;
using NTSCommon.Models;
using NTSTraining.Models.Models.LoaiKhachHang;
using NTSTraining.Services.LoaiKhachHang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolManage.Api.Controllers;

namespace NTSTraining.Api.Controllers
{
    [ApiController]
    [Route("api/customer-type")]
    public class LoaiKhachHangController : BaseApiController
    {
        private readonly ILoaiKhachHangService loaiKhachHangService;
        public LoaiKhachHangController(ILoaiKhachHangService _loaiKhachHangService)
        {
            loaiKhachHangService = _loaiKhachHangService;
        }

        /// <summary>
        /// Tìm kiếm loại khách hàng
        /// </summary>
        /// <param name="modelSearch"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("search")]
        public async Task<ActionResult<SearchBaseResultModel<LoaiKhachHangSearchResultModel>>> SearchLoaiKhachHang([FromBody] LoaiKhachHangSearchModel modelSearch)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await loaiKhachHangService.SearchLoaiKhachHangAsync(modelSearch);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Lấy thông tin loại khách hàng theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ActionResult<LoaiKhachHangModel>>> GetLoaiKhachHangById([FromRoute] string id)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };
            apiResultModel.Data = await loaiKhachHangService.GetLoaiKhachHangByIdAsync(id);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Thêm loại khách hàng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResultModel>> CreateLoaiKhachHang([FromBody] LoaiKhachHangCreateModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };
            await loaiKhachHangService.CreateLoaiKhachHangAsync(model);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Cập nhập loại khách hàng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ApiResultModel>> UpdateLoaiKhachHang([FromRoute] string id, [FromBody] LoaiKhachHangModel model)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            await loaiKhachHangService.UpdateLoaiKhachHangAsync(id, model);

            return Ok(apiResultModel);
        }

        /// <summary>
        /// Xóa loại khách hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ApiResultModel>> DeleteLoaiKhachHang([FromRoute] string id)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };
            await loaiKhachHangService.DeleteLoaiKhachHangByIdAsync(id);
            return Ok(apiResultModel);
        }
        //get all loại khách hàng
        [HttpGet]
        public async Task<ActionResult<List<LoaiKhachHangModel>>> GetLoaiKhachHang()
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await loaiKhachHangService.getAllLoaiKhachhang();

            return Ok(apiResultModel);
        }
    }
}
