using Edenrose.web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edenrose.web.Areas.Admin.Controllers
{
    public class BangGiaController : BaseController
    {
        // GET: Admin/BangGia
        public ActionResult Index()
        {
            var filePath = System.Web.HttpContext.Current.Request.MapPath(string.Format("/BangGia"));
            var model = new List<FileInfoModel>();
           foreach(var files in Directory.GetFiles(filePath))
            {
                FileInfo info = new FileInfo(files);
                var fileName = Path.GetFileName(info.FullName);
                var item = new FileInfoModel()
                {
                    Name = fileName,
                };
                model.Add(item);
            }
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            try
            {
                var filePath = System.Web.HttpContext.Current.Request.MapPath(string.Format("/BangGia/{0}",id));
                if (Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    System.IO.File.Delete(filePath);
                    TempData["SuccessMsg"] = "Xóa file thành công";
                    return Json(GetBaseObjectResult(true, "Xóa file thành công"));
                }
                return Json(GetBaseObjectResult(false, "Xóa file thất bại"));
            }
            catch (Exception ex)
            {
                return Json(GetBaseObjectResult(false, "Xảy ra lỗi khi xóa thuộc tính"));
            }
        }
        [HttpPost]
        public bool Upload()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];
                    if (file != null)
                    {
                        var fname = "";
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                            fname = file.FileName;

                        var fileName = DateTime.Now.Ticks + "_" + Path.GetFileName(fname);
                        bool exists = Directory.Exists(Server.MapPath("~/BangGia/"));
                        if (!exists)
                            Directory.CreateDirectory(Server.MapPath("~/BangGia/"));
                        var path = Path.Combine(Server.MapPath("~/BangGia/"), fileName);
                        file.SaveAs(path);
                        TempData["SuccessMsg"] = "Thêm file bảng giá file thành công";
                        return true;
                    }
                }
                catch
                {

                }
            }
            return false;
        }
    }
}