using Edenrose.Common.Enum;
using Edenrose.Data.Service;
using Edenrose.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edenrose.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArticlesService _artilesService;
        private readonly TopicService _topicService;
        private readonly PictureService _pictureService;
        private readonly ConfigService _configService;
        public HomeController()
        {
            _artilesService = new ArticlesService();
            _topicService = new TopicService();
            _pictureService = new PictureService();
            _configService = new ConfigService();
        }
        public ActionResult Index()
        {
            var model = ToModel();
            if(model.TopicChinhSach != null && model.TopicChinhSach.id > 0)
            {
                ViewBag.urlChinhSach = model.TopicChinhSach.Url;
            }
            if (!string.IsNullOrEmpty(model.email))
            {
                ViewBag.email = model.email;
            }
            if (!string.IsNullOrEmpty(model.phone))
            {
                ViewBag.phone = model.phone;
            }
            return View(model);
        }

        private HomeViewModel ToModel()
        {
            try
            {
                var model = new HomeViewModel();
                model.ListTongQuan = _artilesService.GetDataTongQuan();
                model.TopicLienHe = _topicService.GetByKey((int)TypeTopic.LienHe);
                model.TopicTienIch = _topicService.GetByKey((int)TypeTopic.TienIch);
                model.TopicViTri = _topicService.GetByKey((int)TypeTopic.ViTri);
                model.UrlMatBangTienIch = _pictureService.GetByKey((int)TypeTopic.MatBangTienIch).Url;
                model.UrlMatBangTongThe = _pictureService.GetByKey((int)TypeTopic.MatBangTongThe).Url;
                model.MatBang = _topicService.GetByKey((int)TypeTopic.MatBang);
                model.ListSanPham = _artilesService.GetDataSanPham();
                model.ListArticleNew = _artilesService.GetDataTopArticle();
                model.TopicChinhSach = _artilesService.GetByKey((int)TypeArticle.ChinhSach);
                model.email = _configService.GetbyKey("email").Value;
                model.phone = _configService.GetbyKey("phone").Value;
                model.logo = _configService.GetbyKey("logo").Value;
                model.banner = _configService.GetbyKey("banner").Value;
                model.logoarticle = _configService.GetbyKey("logoarticle").Value;
                return model;
            }
            catch
            {
               return new HomeViewModel();
            }
        }
    }
}