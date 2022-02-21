using NTSCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSTraining.Models.Models.Department
{
    public class DepartmentSearchModel : SearchBaseModel
    {
        public string Id { get; set; }
        public string TenPhongBan { get; set; }
    }
}
