using System;
using System.Collections.Generic;
using System.Text;

namespace NTS.Common.Users
{
    public class NtsUserTokenModel
    {
        public string UserId { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string DeviceId { get; set; }
        public string Token { get; set; }
        public int ExpireDateAfter { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
