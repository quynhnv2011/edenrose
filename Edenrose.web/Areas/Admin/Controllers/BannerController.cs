using Edenrose.Common.Enum;
using Edenrose.Data.EF;
using Edenrose.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edenrose.web.Areas.Admin.Controllers
{
    public class BannerController : BaseController
    {
        private readonly TopicService _topicService;
        public BannerController()
        {
            _topicService = new TopicService();
        }
        public ActionResult Index()
        {
            // var model = new TienIchViewModel();
            var model = _topicService.GetByKey((int)TypeTopic.Banner);
            if (model == null || model.id == 0)
            {
                model = new Topic();
                model.key = (int)TypeTopic.Banner;
                model.ListPicture = new List<Picture>();
            }
            model.ListPicture.ForEach(x => x.TmpId = Guid.NewGuid().ToString());
            Session["ListBanner"] = model.ListPicture;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(Topic model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ListPicture = Session["ListBanner"] as List<Picture>;
                    model.ListPicture = ListPicture;
                    model.ListPicture.ForEach(x => x.Key = (int)TypeTopic.Banner);
                    if (model != null)
                    {
                        var result = false;
                        if (model.id == 0)
                        {
                            model.UpdateDate = DateTime.Now;
                            model.key = (int)TypeTopic.Banner;
                            result = _topicService.Add(model);
                        }
                        else
                        {
                            model.UpdateDate = DateTime.Now;
                            model.key = (int)TypeTopic.Banner;
                            result = _topicService.Update(model, (int)TypeTopic.Banner);
                        }
                        if (result)
                            TempData["SuccessMsg"] = "Cập nhật banner thành công";
                        else
                            TempData["ErrorMsg"] = "Cập nhật banner thất bại";
                        return RedirectToAction("Index", "Banner", new { Area = "Admin" });
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

        public ActionResult GetListPicture()
        {
            var model = new List<Picture>();
            if (Session["ListBanner"] != null)
                model = Session["ListBanner"] as List<Picture>;
            return PartialView("_GetListPicture", model);
        }
        public ActionResult AddPicture(Picture picture)
        {
            var model = new List<Picture>();
            if (Session["ListBanner"] != null)
                model = Session["ListBanner"] as List<Picture>;
            picture.TmpId = Guid.NewGuid().ToString();
            model.Add(picture);
            Session["ListBanner"] = model;
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
            if (Session["ListBanner"] != null)
                model = Session["ListBanner"] as List<Picture>;
            DeletePictureItem(tmpId, model);
            Session["ListBanner"] = model;
            return GetListPicture();
        }

        public ActionResult UpdateDisplay(string tmpId, int Value)
        {
            var model = new List<Picture>();
            if (Session["ListBanner"] != null)
                model = Session["ListBanner"] as List<Picture>;
            var obj = model.SingleOrDefault(x => x.TmpId == tmpId);
            if (obj != null) obj.DisplayOrder = Value;
            Session["ListBanner"] = model;
            return GetListPicture();
        }
    }
}