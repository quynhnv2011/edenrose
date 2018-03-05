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
        public HomeController()
        {
            _artilesService = new ArticlesService();
            _topicService = new TopicService();
            _pictureService = new PictureService();
        }
        public ActionResult Index()
        {
            var model = ToModel();
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
                model.ListSanPham = _artilesService.GetDataSanPham();
                model.ListArticleNew = _artilesService.GetDataTopArticle();
                return model;
            }
            catch
            {
               return new HomeViewModel();
            }
        }
    }
}