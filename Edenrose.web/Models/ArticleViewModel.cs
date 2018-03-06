using Edenrose.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edenrose.web.Models
{
    public class ArticleViewModel: BaseViewModel
    {
        public ArticleViewModel()
        {
            ListMoiNhat = new List<Article>();
            ListData = new List<Article>();
        }
        public List<Article> ListMoiNhat { get; set; }
        public List<Article> ListData { get; set; }
    }
}