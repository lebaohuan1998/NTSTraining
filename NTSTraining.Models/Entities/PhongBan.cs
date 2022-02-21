using System;
using System.Collections.Generic;

#nullable disable

namespace NTSTraining.Models.Entities
{
    public partial class PhongBan
    {
        public PhongBan()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        public string Id { get; set; }
        public string TenPhongBan { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
