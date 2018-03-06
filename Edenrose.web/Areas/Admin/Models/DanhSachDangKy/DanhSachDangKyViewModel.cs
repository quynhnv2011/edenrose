using Edenrose.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edenrose.web.Areas.Admin.Models
{
    public class DanhSachDangKyViewModel : BaseListModel
    {
        public DanhSachDangKyViewModel()
        {
            ListData = new List<Resignation>();
        }
        public List<Resignation> ListData { get; set; }
       
    }
}