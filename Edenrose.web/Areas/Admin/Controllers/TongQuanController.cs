using AutoMapper;
using Edenrose.Common.Enum;
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
    public class TongQuanController : BaseController
    {
        private readonly ArticlesService _artilesService;
        public TongQuanController()
        {
            _artilesService = new ArticlesService();
        }
        public ActionResult Index(TongQuanViewModel model)
        {
            int totalItem;
            var allCategory = _artilesService.GetData(model.Title, model.PageIndex, model.PageSize, out totalItem, (int)TypeArticle.TongQuan).ToList();
            model.TotalItem = totalItem;
            model.ListData = allCategory;
            return View(model);
        }

        public ActionResult Create()
        {
            TongQuanItemModel model = new TongQuanItemModel();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TongQuanItemModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {                   
                    if (model != null)
                    {
                        var modelItem = model.ToModel();
                     
                      //  var modelItem = Mapper.Map<Article>(model);
                        if (model.id == 0)
                        {

                            modelItem.CreatedBy = 1;
                            modelItem.CreatedDate = DateTime.Now;
                            modelItem.Deleted = false;
                            model.Visits = 0;
                            modelItem.TypeArticle = (int)Common.Enum.TypeArticle.TongQuan;
                            var result = _artilesService.Add(modelItem);
                            if (result)
                                TempData["SuccessMsg"] = "Thêm mới bài viết tổng quan thành công";
                            else
                                TempData["ErrorMsg"] = "Thêm mới bài viết tổng quan thất bại";
                        }
                        else
                        {
                            modelItem.Deleted = false;
                            modelItem.UpdatedDate = DateTime.Now;
                            var result = _artilesService.Update(modelItem);
                            if (result)
                                TempData["SuccessMsg"] = "Cập nhật bài viết thành công";
                            else
                                TempData["ErrorMsg"] = "Cập nhật bài viết thất bại";
                        }
                        return RedirectToAction("Index", "TongQuan", new { Area = "Admin" });
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
            TongQuanItemModel model = new TongQuanItemModel();
            var categoryItem = _artilesService.GetById(id);
            model = categoryItem.ToModel();
            return View("Create", model);
        }

        [EncryptedActionParameter]
        public ActionResult Delete(int id)
        {
            try
            {
                var objArticle = _artilesService.GetById(id);
                objArticle.Deleted = true;
                var _result = _artilesService.Update(objArticle);
                if (_result)
                {
                    TempData["SuccessMsg"] = "Xóa thuộc tính thành công";
                    return Json(GetBaseObjectResult(true, "Xóa bài viết thành công"));
                }
                return Json(GetBaseObjectResult(false, "Xóa bài viết thất bại"));
            }
            catch (Exception ex)
            {
                return Json(GetBaseObjectResult(false, "Xảy ra lỗi khi xóa thuộc tính"));
            }
        }
    }
}