using Edenrose.Data.EF;
using Edenrose.Data.Service;
using Edenrose.web.Areas.Admin.Models;
using Edenrose.web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edenrose.web.Areas.Admin.Controllers
{
    public class ConfigController : BaseController
    {
        private readonly ConfigService _configService;
        public ConfigController()
        {
            _configService = new ConfigService();
        }
        public ActionResult Index()
        {
            var model = new ConfigViewModel();
            int totalItem;
            var allCategory = _configService.GetData(model.PageIndex, model.PageSize, out totalItem).ToList();
            model.TotalItem = totalItem;
            model.ListData = allCategory;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Config model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model != null)
                    {
                        if (model.id == 0)
                        {

                            var result = _configService.Add(model);
                            if (result)
                                TempData["SuccessMsg"] = "Thêm mới cấu hình thành công";
                            else
                                TempData["ErrorMsg"] = "Thêm mới cấu hình thất bại";
                        }
                        else
                        {
                            var result = _configService.Update(model);
                            if (result)
                                TempData["SuccessMsg"] = "Cập nhật cấu hình thành công";
                            else
                                TempData["ErrorMsg"] = "Cập nhật cấu hình thất bại";
                        }
                        return RedirectToAction("Index", "Config", new { Area = "Admin" });
                    }
                    return Json(GetBaseObjectResult(false, "Thực hiện thất bại"));
                }
                catch (Exception ex)
                {
                    return Json(GetBaseObjectResult(false, "Xảy ra lỗi. Bạn vui lòng thử lại sau !"));
                }
            }
            return Json(GetBaseObjectResult(false, "Dữ liệu không hợp lệ"));
        }

        [EncryptedActionParameter]
        public ActionResult ViewEdit(int id)
        {
            var model = _configService.GetbyId(id);
            return View("Create", model);
        }
    }
}