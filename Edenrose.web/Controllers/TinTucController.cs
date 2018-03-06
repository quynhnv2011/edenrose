using Edenrose.Data.EF;
using Edenrose.Data.Service;
using Edenrose.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edenrose.web.Controllers
{
    public class TinTucController : Controller
    {
        private readonly ResignService _resignService;
        private readonly ArticlesService _artilesService;
        public TinTucController()
        {
            _artilesService = new ArticlesService();
            _resignService = new ResignService();
        }
        public ActionResult Index(string pageindex)
        {
            return View();
        }
        public ActionResult Details(string url)
        {
            var model = new DetailViewModel();
            if (!string.IsNullOrEmpty(url))
            {
                model.ArtileDetail = _artilesService.GetByUrl(url);

            }
            model.ListMoiNhat = _artilesService.GetDataNew();
            model.ListCungChuDe = _artilesService.GetDataCungChuDe();
            return View(model);
        }

        [HttpPost]
        public bool Resign(string email, string name, string phone)
        {
            try
            {
                var obj = new Resignation
                {
                    Email = email,
                    Phone = phone,
                    Name = name,
                    CreatedDate = DateTime.Now
                };
                var result = _resignService.Add(obj);
                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}