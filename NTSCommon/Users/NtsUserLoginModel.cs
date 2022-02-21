using System;
using System.Collections.Generic;
using System.Text;

namespace NTS.Common.Users
{
    public class NtsUserLoginModel
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string EmployeeId { get; set; }
        public string Password { get; set; }
        public string SecurityStamp { get; set; }
        public string PasswordHash { get; set; }
        public string Secret { get; set; }
        public int ExpireDateAfter { get; set; }
        public string DeviceId { get; set; }
    }
}
