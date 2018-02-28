using Edenrose.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edenrose.web.Areas.Admin.Models
{
    public class ArticlesViewModel: BaseListModel
    {
        public ArticlesViewModel()
        {
            ListData = new List<Article>();
        }
        public string Title { get; set; }
        public List<Article> ListData { get; set; }
    }
}