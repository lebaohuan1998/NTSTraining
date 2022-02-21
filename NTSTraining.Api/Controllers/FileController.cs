using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;
using NTS.Common.Files;
using NTS.Common.Models;
using NTS.Common.RedisCache;
using ToolManage.Models.Models.Settings;

namespace ToolManage.Api.Controllers
{
    [ApiController]
    [Route("api/file")]
    public class FileController : BaseApiController
    {
        private readonly AppSettingModel _appSettingModel;
        private readonly IUploadFileService uploadFileService;
        public FileController(IOptions<AppSettingModel> appSettingOptionss, IUploadFileService uploadFileService)
        {
            this._appSettingModel = appSettingOptionss.Value;
            this.uploadFileService = uploadFileService;
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        [Route("upload-file")]
        [HttpPost]
        public async Task<ActionResult<ApiResultModel<UploadResultModel>>> UploadFile([FromForm] IFormFile file, [FromForm] string folderName)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await uploadFileService.UploadFile(file, folderName);

            return Ok(apiResultModel);
        }

        [Route("upload-files")]
        [HttpPost]
        public async Task<ActionResult<ApiResultModel<UploadResultModel>>> UploadFile([FromForm] List<IFormFile> files, [FromForm] string folderName)
        {
            ApiResultModel apiResultModel = new ApiResultModel
            {
                StatusCode = ApiResultConstants.StatusCodeSuccess
            };

            apiResultModel.Data = await uploadFileService.UploadFiles(files, folderName);

            return Ok(apiResultModel);
        }

        [HttpPost]
        [Route("download-files")]
        public async Task<IActionResult> DownloadMedia(DownloadFileModel downloadFileModel)
        {
            try
            {
                var file = await uploadFileService.DownloadFiles(downloadFileModel);

                return File(file.FileStream, file.ContentType, file.FileName);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("download-file")]
        public async Task<IActionResult> DownloadFiles(FileModel fileModel)
        {
            try
            {
                var file = await uploadFileService.DownloadFile(fileModel);

                return File(file.FileStream, file.ContentType, file.FileName);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}