using System;
using System.Collections.Generic;

#nullable disable

namespace NTSTraining.Models.Entities
{
    public partial class KhachHang
    {
        public string Id { get; set; }
        public string TenKhachHang { get; set; }
        public string MaKhachHang { get; set; }
        public string LoaiKhachHangId { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string GhiChu { get; set; }

        public virtual LoaiKhachHang LoaiKhachHang { get; set; }
    }
}
