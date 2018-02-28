using Edenrose.web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Edenrose.web.Areas.Admin.Account
{
    public class LoginModel : BaseModel
    {
        [Required(ErrorMessage = "Anh/Chị chưa nhập tài khoản")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Anh/Chị chưa nhập mật khẩu")]
        public string Password { get; set; }
        public string Capcha { get; set; }
        public string RedirectUrl { get; set; }
    }
}