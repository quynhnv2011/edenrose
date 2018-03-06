using Edenrose.Data.Service;
using Edenrose.web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edenrose.web.Areas.Admin.Controllers
{
    public class DanhSachDangKyController : BaseController
    {
        private readonly ResignService _resignService;
        public DanhSachDangKyController()
        {
            _resignService = new ResignService();
        }
        public ActionResult Index()
        {
            var model = new DanhSachDangKyViewModel();
            int totalItem;
            var allCategory = _resignService.GetData(model.PageIndex, model.PageSize, out totalItem).ToList();
            model.TotalItem = totalItem;
            model.ListData = allCategory;
            return View(model);
        }
    }
}