using NTS.Common.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTS.Common.Users
{
    public class NtsLogInModel
    {
        /// <summary>
        /// Tài khoản
        /// </summary>
        [Required(ErrorMessageResourceName = MessageResourceKey.MSG0014, ErrorMessageResourceType = typeof(MessageResource))]
        [Display(Name = "Username")]
        [MaxLength(100, ErrorMessageResourceName = MessageResourceKey.MSG0016, ErrorMessageResourceType = typeof(MessageResource))]
        public string Username { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Required(ErrorMessageResourceName = MessageResourceKey.MSG0014, ErrorMessageResourceType = typeof(MessageResource))]
        [Display(Name = "Username")]
        [MaxLength(64, ErrorMessageResourceName = MessageResourceKey.MSG0016, ErrorMessageResourceType = typeof(MessageResource))]
        public string Password { get; set; }
    }
}
