using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSTraining.Models.Models.KhachHang
{
    public class KhachHangCreateModel
    {
        public string Id { get; set; }
        public string TenKhachHang { get; set; }
        public string MaKhachHang { get; set; }
        public string LoaiKhachHangId { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string GhiChu { get; set; }
    }
}
