using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Edenrose.web.Areas.Admin.Models
{
    public class SeoConfigItem
    {
        [Required(ErrorMessage ="Mời bạn nhập tiêu đề website")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập mô tả website")]
        public string Description { get; set; }

        public int IdTitle { get; set; }
        public int IdDescription { get; set; }
    }
}