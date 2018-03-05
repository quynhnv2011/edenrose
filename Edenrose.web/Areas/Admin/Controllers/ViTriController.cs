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
    public class ViTriController : BaseController
    {
        private readonly TopicService _topicService;
        public ViTriController()
        {
            _topicService = new TopicService();
        }
        // GET: Admin/ViTri
        public ActionResult Index()
        {
            var model = _topicService.GetByKey((int)TypeTopic.ViTri);
            if (model == null || model.id == 0)
            {
                model = new Topic();
                model.key = (int)TypeTopic.ViTri;
            }
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
                    if (model != null)
                    {
                        var result = false;
                        if (model.id == 0)
                        {
                            model.UpdateDate = DateTime.Now;
                            model.key = (int)TypeTopic.ViTri;
                            result = _topicService.Add(model);
                        }
                        else
                        {
                            model.UpdateDate = DateTime.Now;
                            model.key = (int)TypeTopic.ViTri;
                            result = _topicService.Update(model);
                        }
                        if (result)
                            TempData["SuccessMsg"] = "Cập nhật trang vị trí thành công";
                        else
                            TempData["ErrorMsg"] = "Cập nhật trang vị trí thất bại";
                        return RedirectToAction("Index", "ViTri", new { Area = "Admin" });
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
    }
}