using Edenrose.Data.EF;
using Edenrose.Data.Service;
using Edenrose.web.Models;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vks.Common.Utils;

namespace Edenrose.web.Controllers
{
    public class TinTucController : Controller
    {
        private readonly ResignService _resignService;
        private readonly ArticlesService _artilesService;
        private readonly ConfigService _configService;
        public TinTucController()
        {
            _artilesService = new ArticlesService();
            _resignService = new ResignService();
            _configService = new ConfigService();
        }
        public ActionResult Index(int pageindex = 1)
        {
            var model = new ArticleViewModel();
            int totalItem = 0;
            model.ListData = _artilesService.GetData(pageindex, 10, out totalItem);
            model.TotalItem = totalItem;
            model.PageIndex = pageindex;
            model.ListMoiNhat = _artilesService.GetDataNew();
            var  modelEmail = _configService.GetbyKey("email").Value;
            var modelPhone = _configService.GetbyKey("phone").Value;
            if (!string.IsNullOrEmpty(modelEmail))
            {
                ViewBag.email = modelEmail;
            }
            if (!string.IsNullOrEmpty(modelPhone))
            {
                ViewBag.phone = modelPhone;
            }
            return View(model);
        }
        public ActionResult Details(string url)
        {
            var model = new DetailViewModel();
            if (!string.IsNullOrEmpty(url))
            {
                model.ArtileDetail = _artilesService.GetByUrl(url);
            }
            var modelEmail = _configService.GetbyKey("email").Value;
            var modelPhone = _configService.GetbyKey("phone").Value;
            if (!string.IsNullOrEmpty(modelEmail))
            {
                ViewBag.email = modelEmail;
            }
            if (!string.IsNullOrEmpty(modelPhone))
            {
                ViewBag.phone = modelPhone;
            }
            model.ListMoiNhat = _artilesService.GetDataNew();
            model.ListCungChuDe = _artilesService.GetDataCungChuDe();
            return View(model);
        }

       
        [HttpGet]
        public ActionResult Resign(string email, string name, string phone)
        {
            try
            {
                var obj = new Resignation
                {
                    Email = email,
                    Phone = phone,
                    Name = name,
                    CreatedDate = DateTime.Now
                };
                var result = _resignService.Add(obj);
                var FolderPath = System.Web.HttpContext.Current.Request.MapPath(string.Format("/BangGia"));

                //Define file Type
                string fileType = "application/octet-stream";

                //Define Output Memory Stream
                var outputStream = new MemoryStream();

                //Create object of ZipFile library
                using (ZipFile zipFile = new ZipFile())
                {
                    //Add Root Directory Name "Files" or Any string name
                    zipFile.AddDirectoryByName("Files");

                    //Get all filepath from folder
                    String[] files = Directory.GetFiles(FolderPath);
                    foreach (string file in files)
                    {
                        string filePath = file;
                        //Adding files from filepath into Zip
                        zipFile.AddFile(filePath, "Files");
                    }

                    Response.ClearContent();
                    Response.ClearHeaders();

                    //Set zip file name
                    Response.AppendHeader("content-disposition", "attachment; filename=BangGia.zip");

                    //Save the zip content in output stream
                    zipFile.Save(outputStream);
                }

                //Set the cursor to start position
                outputStream.Position = 0;

                //Dispance the stream
                return new FileStreamResult(outputStream, fileType);
            }
            catch(Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
               throw;
            }
        }
    }
}