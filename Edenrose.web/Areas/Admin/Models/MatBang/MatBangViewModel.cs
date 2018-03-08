using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edenrose.web.Areas.Admin.Models
{
    public class MatBangViewModel
    {
        public int Id { get; set; }
        public string MoTa { get; set; }
        public string MatBangChiTiet { get; set; }
        public int MatBangTienIchId { get; set; }
        public string UrlMatBangTienIch { get; set; }
        public int MatBangTongTheId { get; set; }
        public string UrlMatBangTongThe { get; set; }
    }
}