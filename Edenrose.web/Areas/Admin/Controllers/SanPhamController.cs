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
    public class SanPhamController : BaseController
    {
        private readonly ArticlesService _artilesService;
        public SanPhamController()
        {
            _artilesService = new ArticlesService();
        }
        public ActionResult Index(ArticlesViewModel model)
        {
            int totalItem;
            var allCategory = _artilesService.GetData(model.Title, model.PageIndex, model.PageSize, out totalItem, (int)TypeArticle.SanPham).ToList();
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
                    var ListPicture = Session["ListPictureProduct"] as List<Picture>;
                    if (model != null)
                    {
                        var modelItem = model.ToModel();

                        modelItem.ListPicture = ListPicture;
                        modelItem.ListPicture.ForEach(x => x.Key = (int)TypeTopic.Product);
                        //  var modelItem = Mapper.Map<Article>(model);
                        if (model.id == 0)
                        {

                            modelItem.CreatedBy = 1;
                            modelItem.CreatedDate = DateTime.Now;
                            modelItem.Deleted = false;
                            model.Visits = 0;
                            modelItem.TypeArticle = (int)Common.Enum.TypeArticle.SanPham;
                            var result = _artilesService.Add(modelItem);
                            if (result)
                                TempData["SuccessMsg"] = "Thêm mới sản phẩm thành công";
                            else
                                TempData["ErrorMsg"] = "Thêm mới sản phẩm thất bại";
                        }
                        else
                        {
                            modelItem.Deleted = false;
                            modelItem.UpdatedDate = DateTime.Now;
                            modelItem.ListPicture.ForEach(x => x.ReferenceId = modelItem.id);
                            var result = _artilesService.Update(modelItem, (int)TypeTopic.Product);
                            if (result)
                                TempData["SuccessMsg"] = "Cập nhật sản phẩm thành công";
                            else
                                TempData["ErrorMsg"] = "Cập nhật sản phẩm thất bại";
                        }
                        return RedirectToAction("Index", "SanPham", new { Area = "Admin" });
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
            var categoryItem = _artilesService.GetById(id, (int)TypeTopic.Product);
            Session["ListPictureProduct"] = categoryItem.ListPicture;
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

        public ActionResult GetListPicture()
        {
            var model = new List<Picture>();
            if (Session["ListPictureProduct"] != null)
                model = Session["ListPictureProduct"] as List<Picture>;
            return PartialView("_GetListPicture", model);
        }
        public ActionResult AddPicture(Picture picture)
        {
            var model = new List<Picture>();
            if (Session["ListPictureProduct"] != null)
                model = Session["ListPictureProduct"] as List<Picture>;
            picture.TmpId = Guid.NewGuid().ToString();
            model.Add(picture);
            Session["ListPictureProduct"] = model;
            return GetListPicture();
        }

        private bool DeletePictureItem(string tmpId, List<Picture> listPicture)
        {
            try
            {
                var obj = listPicture.Find(x => x.TmpId == tmpId);
                listPicture.Remove(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public ActionResult DeletePicture(string tmpId)
        {
            var model = new List<Picture>();
            if (Session["ListPictureProduct"] != null)
                model = Session["ListPictureProduct"] as List<Picture>;
            DeletePictureItem(tmpId, model);
            Session["ListPictureProduct"] = model;
            return GetListPicture();
        }

        public ActionResult UpdateDisplay(string tmpId, int Value)
        {
            var model = new List<Picture>();
            if (Session["ListPictureProduct"] != null)
                model = Session["ListPictureProduct"] as List<Picture>;
            var obj = model.SingleOrDefault(x => x.TmpId == tmpId);
            if (obj != null) obj.DisplayOrder = Value;
            Session["ListPictureProduct"] = model;
            return GetListPicture();
        }
    }
}