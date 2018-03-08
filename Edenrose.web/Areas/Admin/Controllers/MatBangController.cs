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
    public class MatBangController : BaseController
    {
        private readonly TopicService _topicService;
        private readonly PictureService _pictureService;
        public MatBangController()
        {
            _topicService = new TopicService();
            _pictureService = new PictureService();
        }
        public ActionResult Index()
        {

            var modelView = new MatBangViewModel();
            var matBangTienIch = _pictureService.GetByKey((int)TypeTopic.MatBangTienIch);
            if (matBangTienIch != null)
            {
                modelView.MatBangTienIchId = matBangTienIch.id;
                modelView.UrlMatBangTienIch = matBangTienIch.Url;
            }
            var matBangTongThe = _pictureService.GetByKey((int)TypeTopic.MatBangTongThe);
            if (matBangTongThe != null)
            {
                modelView.MatBangTongTheId = matBangTongThe.id;
                modelView.UrlMatBangTongThe = matBangTongThe.Url;
            }
            var model = _topicService.GetByKey((int)TypeTopic.MatBang);
            if (model != null && model.id > 0)
            {
                modelView.MoTa = model.ShortDescription;
                modelView.Id = model.id;
                modelView.MatBangChiTiet = model.ContentDetail;
            }
            return View(modelView);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(MatBangViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ListPicture = new List<Picture>();
                    if (model != null)
                    {
                        var result = false;
                        if (model.Id > 0)
                        {
                            var obj = _topicService.GetById(model.Id);
                            obj.ShortDescription = model.MoTa;
                            obj.ContentDetail = model.MatBangChiTiet;
                            _topicService.Update(obj);

                        }
                        else
                        {
                            var obj = new Topic
                            {
                                ShortDescription = model.MoTa,
                                ContentDetail = model.MatBangChiTiet,
                                key = (int)TypeTopic.MatBang
                            };
                            _topicService.Add(obj);
                        }
                        var matBangtienIch = new Picture
                        {
                            id = model.MatBangTienIchId,
                            Url = model.UrlMatBangTienIch,
                            Key = (int)TypeTopic.MatBangTienIch

                        };
                        ListPicture.Add(matBangtienIch);
                        var matBangtongThe = new Picture
                        {
                            id = model.MatBangTongTheId,
                            Url = model.UrlMatBangTongThe,
                            Key = (int)TypeTopic.MatBangTongThe

                        };
                        ListPicture.Add(matBangtongThe);
                        result = _pictureService.UpdateMatBang(ListPicture);

                        if (result)
                            TempData["SuccessMsg"] = "Cập nhật trang mặt bằng thành công";
                        else
                            TempData["ErrorMsg"] = "Cập nhật trang mặt bằng thất bại";
                        return RedirectToAction("Index", "MatBang", new { Area = "Admin" });
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