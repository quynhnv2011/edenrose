using Edenrose.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edenrose.web.Areas.Admin.Models
{
    public class TienIchViewModel
    {
        public TienIchViewModel()
        {
            ListPicture = new List<Picture>();
        }
        public string Mota { get; set; }
        public string Picture { get; set; }
        public List<Picture> ListPicture { get; set; }
    }
}