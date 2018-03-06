using Edenrose.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edenrose.web.Models
{
    public class DetailViewModel
    {
        public DetailViewModel()
        {
            ArtileDetail = new Article();
            ListMoiNhat = new List<Article>();
            ListCungChuDe = new List<Article>();
        }
        public Article ArtileDetail { get; set; }
        public List<Article> ListMoiNhat { get; set; }
        public List<Article> ListCungChuDe { get; set; }
    }
}