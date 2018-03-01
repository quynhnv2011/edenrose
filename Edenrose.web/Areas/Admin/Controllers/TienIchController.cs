using Edenrose.Common.Enum;
using Edenrose.Data.EF;
using Edenrose.Data.Service;
using Edenrose.web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edenrose.web.Areas.Admin.Controllers
{
    public class TienIchController : BaseController
    {
        private readonly TopicService _topicService;
        public TienIchController()
        {
            _topicService = new TopicService();
        }
        public ActionResult Index()
        {
           // var model = new TienIchViewModel();
            var model = _topicService.GetByKey((int)TypeTopic.TienIch);
            if (model == null || model.id == 0)
            {
                model = new Topic();
                model.key = (int)TypeTopic.TienIch;
                model.ListPicture = new List<Picture>();
            }
            model.ListPicture.ForEach(x => x.TmpId = Guid.NewGuid().ToString());
            Session["ListPicture"] = model.ListPicture;
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(Topic model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ListPicture = Session["ListPicture"] as List<Picture>;
                    model.ListPicture = ListPicture;
                    model.ListPicture.ForEach(x => x.Key = (int)TypeTopic.TienIch);
                    if (model != null)
                    {
                        var result = false;
                        if (model.id == 0)
                        {
                            model.UpdateDate = DateTime.Now;
                            model.key = (int)TypeTopic.TienIch;
                            result = _topicService.Add(model);
                        }
                        else
                        {
                            model.UpdateDate = DateTime.Now;
                            model.key = (int)TypeTopic.TienIch;
                            result = _topicService.Update(model, (int)TypeTopic.TienIch);
                        }
                        if (result)
                            TempData["SuccessMsg"] = "Cập nhật trang tiện ích thành công";
                        else
                            TempData["ErrorMsg"] = "Cập nhật trang tiện ích thất bại";
                        return RedirectToAction("Index", "TienIch", new { Area = "Admin" });
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
            if (Session["ListPicture"] != null)
                model = Session["ListPicture"] as List<Picture>;
            return PartialView("_GetListPicture", model);
        }
        public ActionResult AddPicture(Picture picture)
        {
            var model = new List<Picture>();
            if (Session["ListPicture"] != null)
                model = Session["ListPicture"] as List<Picture>;
            picture.TmpId = Guid.NewGuid().ToString();
            model.Add(picture);
            Session["ListPicture"] = model;
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
            if (Session["ListPicture"] != null)
                model = Session["ListPicture"] as List<Picture>;
            DeletePictureItem(tmpId, model);
            Session["ListPicture"] = model;
            return GetListPicture();
        }

        public ActionResult UpdateDisplay(string tmpId, int Value)
        {
            var model = new List<Picture>();
            if (Session["ListPicture"] != null)
                model = Session["ListPicture"] as List<Picture>;
            var obj = model.SingleOrDefault(x=>x.TmpId == tmpId);
            if (obj != null) obj.DisplayOrder = Value;
            Session["ListPicture"] = model;
            return GetListPicture();
        }
    }
}