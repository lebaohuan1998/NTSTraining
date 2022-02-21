using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTSCommon.Models;
using NTSTraining.Models.Models.KhachHang;


namespace NTSTraining.Services.KhachHang
{
    public interface IKhachHangService
    {
        Task CreateKhachHangAsync(KhachHangCreateModel model);
        Task<SearchBaseResultModel<KhachHangSearchModel>> SearchKhachHangAsync(KhachHangSearchModel modelSearch);
        Task<KhachHangCreateModel> GetKhachHangById(string id);
        Task DeleteKhachHangByIdAsync(string id);
        Task UpdateKhachHangAsync(string id, KhachHangCreateModel model);
    }
}
