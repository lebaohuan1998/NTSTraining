using System;
using System.Collections.Generic;

#nullable disable

namespace NTSTraining.Models.Entities
{
    public partial class NhanVien
    {
        public string Id { get; set; }
        public string HoVaTen { get; set; }
        public string GioiTinh { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string Cmnd { get; set; }
        public string PhongBanId { get; set; }

        public virtual PhongBan PhongBan { get; set; }
    }
}
