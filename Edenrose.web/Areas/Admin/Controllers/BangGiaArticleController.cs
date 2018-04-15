using Edenrose.Common.Enum;
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
    public class BangGiaArticleController : BaseController
    {
        private readonly ArticlesService _artilesService;
        public BangGiaArticleController()
        {
            _artilesService = new ArticlesService();
        }
        public ActionResult Index(ArticlesViewModel model)
        {
            int totalItem;
            var allCategory = _artilesService.GetData(model.Title, model.PageIndex, model.PageSize, out totalItem, (int)TypeArticle.BangGia).ToList();
            model.TotalItem = totalItem;
            model.ListData = allCategory;
            return View(model);
        }

        public ActionResult Create()
        {
            ArticleItemModel model = new ArticleItemModel();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ArticleItemModel model)
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
                            modelItem.TypeArticle = (int)Common.Enum.TypeArticle.BangGia;
                            var result = _artilesService.Add(modelItem);
                            if (result)
                                TempData["SuccessMsg"] = "Thêm mới bảng giá thành công";
                            else
                                TempData["ErrorMsg"] = "Thêm mới bảng giá thất bại";
                        }
                        else
                        {
                            modelItem.Deleted = false;
                            modelItem.UpdatedDate = DateTime.Now;
                            var result = _artilesService.Update(modelItem);
                            if (result)
                                TempData["SuccessMsg"] = "Cập nhật bảng giá thành công";
                            else
                                TempData["ErrorMsg"] = "Cập nhật bảng giá thất bại";
                        }
                        return RedirectToAction("Index", "BangGiaArticle", new { Area = "Admin" });
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
            ArticleItemModel model = new ArticleItemModel();
            var categoryItem = _artilesService.GetById(id);
            model = categoryItem.ToModelArticle();
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
                    TempData["SuccessMsg"] = "Xóa bảng giá thành công";
                    return Json(GetBaseObjectResult(true, "Xóa bảng giá thành công"));
                }
                return Json(GetBaseObjectResult(false, "Xóa bảng giá thất bại"));
            }
            catch (Exception ex)
            {
                return Json(GetBaseObjectResult(false, "Xảy ra lỗi khi xóa bảng giá"));
            }
        }
    }
}