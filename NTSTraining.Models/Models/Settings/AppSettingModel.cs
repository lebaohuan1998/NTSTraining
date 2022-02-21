using System;
using System.Collections.Generic;
using System.Text;

namespace ToolManage.Models.Models.Settings
{
    public class AppSettingModel
    {
        public string Secret { get; set; }
        public int ExpireDateAfter { get; set; }
        public string ServerFileUrl { get; set; }
        public string KeyAuthorize { get; set; }
        public string SubWebUrl { get; set; }
    }
}
