using System;
using System.Collections.Generic;

#nullable disable

namespace NTSTraining.Models.Entities
{
    public partial class LoaiKhachHang
    {
        public LoaiKhachHang()
        {
            KhachHangs = new HashSet<KhachHang>();
        }

        public string Id { get; set; }
        public string TenLoaiKhachHang { get; set; }

        public virtual ICollection<KhachHang> KhachHangs { get; set; }
    }
}
