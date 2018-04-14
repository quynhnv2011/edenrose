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
                        return Redirect(model.Name.Trim().ToLower());
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
        public ActionResult Phone()
        {
            var model = _configService.GetbyKey("phone");
            if (model == null)
            {
                model = new Config();
                model.Name = "phone";
            };
            return View("Create", model);
        }

        public ActionResult Email()
        {
            var model = _configService.GetbyKey("email");
            if (model == null)
            {
                model = new Config();
                model.Name = "email";
            };
            return View("Create", model);
        }


        public ActionResult Logo()
        {
            var model = _configService.GetbyKey("logo");
            if (model == null)
            {
                model = new Config();
                model.Name = "logo";
            };
            return View("_CreateFile",model);
        }

        public ActionResult Banner()
        {
            var model = _configService.GetbyKey("banner");
            if (model == null)
            {
                model = new Config();
                model.Name = "banner";
            };
            return View("_CreateFile", model);
        }
        public ActionResult LogoArticle()
        {
            var model = _configService.GetbyKey("logoarticle");
            if (model == null)
            {
                model = new Config();
                model.Name = "logoarticle";
            };
            return View("_CreateFile", model);
        }

        public ActionResult CauHinhSeo()
        {
            var model = new SeoConfigItem();
            model.Title = _configService.GetbyKey("title") != null ? _configService.GetbyKey("title").Value : string.Empty;
            model.IdTitle = _configService.GetbyKey("title").id;
            model.Description = _configService.GetbyKey("description") != null ? _configService.GetbyKey("description").Value: string.Empty;
            model.IdDescription = _configService.GetbyKey("description").id;
            return View(model);
        }
        [HttpPost]
        public ActionResult CauHinhSeo(SeoConfigItem model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model != null)
                    {
                        if (model.IdTitle == 0 && model.IdDescription == 0)
                        {
                            var objTitle = new Config()
                            {
                                Name = "title",
                                Value = model.Title
                            };

                            var result = _configService.Add(objTitle);
                            var objDesciption= new Config()
                            {
                                Name = "description",
                                Value = model.Description
                            };
                            result = _configService.Add(objDesciption);
                            if (result)
                                TempData["SuccessMsg"] = "Thêm mới cấu hình thành công";
                            else
                                TempData["ErrorMsg"] = "Thêm mới cấu hình thất bại";
                        }
                        else
                        {
                            var objTitle = new Config()
                            {
                                Name = "title",
                                id = model.IdTitle,
                                Value = model.Title
                            };

                            var result = _configService.Update(objTitle);
                            var objDesciption = new Config()
                            {
                                Name = "description",
                                id = model.IdDescription,
                                Value = model.Description
                            };
                            result = _configService.Update(objDesciption);
                            if (result)
                                TempData["SuccessMsg"] = "Cập nhật cấu hình thành công";
                            else
                                TempData["ErrorMsg"] = "Cập nhật cấu hình thất bại";
                        }
                        return RedirectToAction("CauHinhSeo");
                    }
                    return Json(GetBaseObjectResult(false, "Thực hiện thất bại"));
                }
                catch (Exception ex)
                {
                    return Json(GetBaseObjectResult(false, "Xảy ra lỗi. Bạn vui lòng thử lại sau !"));
                }
            }
            return View(model);
        }

        private RedirectToRouteResult Redirect(string name)
        {
            switch (name)
            {
                case "logo":
                    return RedirectToAction("logo", "Config", new { Area = "Admin" });
                case "phone":
                    return RedirectToAction("phone", "Config", new { Area = "Admin" });
                case "email":
                    return RedirectToAction("email", "Config", new { Area = "Admin" });
                case "banner":
                    return RedirectToAction("banner", "Config", new { Area = "Admin" });
                case "logoarticle":
                    return RedirectToAction("logoarticle", "Config", new { Area = "Admin" });
                default:
                    return RedirectToAction("Index", "Config", new { Area = "Admin" });

            }
        }
    }
}