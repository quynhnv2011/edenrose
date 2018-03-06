using Edenrose.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edenrose.web.Models
{
    public class HomeViewModel
    {
        public List<Article> ListTongQuan { get; set; }
        public Topic TopicLienHe { get; set; }
        public Topic TopicViTri { get; set; }
        public Topic TopicTienIch { get; set; }
        public Article TopicChinhSach { get; set; }
        public string UrlMatBangTongThe { get; set; }
        public string UrlMatBangTienIch { get; set; }
        public List<Article> ListSanPham { get; set; }
        public List<Article> ListArticleNew { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}