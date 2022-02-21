using NTSCommon.Models;
using NTSTraining.Models.Models.LoaiKhachHang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSTraining.Services.LoaiKhachHang
{
    public interface ILoaiKhachHangService
    {
        Task<SearchBaseResultModel<LoaiKhachHangSearchResultModel>> SearchLoaiKhachHangAsync(LoaiKhachHangSearchModel searchModel);

        Task CreateLoaiKhachHangAsync(LoaiKhachHangCreateModel model);

        Task DeleteLoaiKhachHangByIdAsync(string id);

        Task<LoaiKhachHangModel> GetLoaiKhachHangByIdAsync(string id);

        Task UpdateLoaiKhachHangAsync(string id, LoaiKhachHangModel model);

        Task<List<LoaiKhachHangModel>> getAllLoaiKhachhang();
    }
}
