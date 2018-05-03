using Edenrose.Data.EF;
using Edenrose.Data.Service;
using Edenrose.web.Models;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
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

                //gửi email về quản trị

                try
                {

                    var body = new StringBuilder();
                    body.AppendFormat("<p>Kính gửi quản trị viên website Edenrose.vn,{0}</p>", Environment.NewLine);
                    body.AppendFormat("<p>Hệ thống website Edenrose.vn vừa nhận được dữ liệu đăng kí với thông tin:{0}</p>", Environment.NewLine);
                    body.AppendFormat("<p>- Họ và tên: {0}.{1}</p>", obj.Name, Environment.NewLine);
                    body.AppendFormat("<p>- Số điện thoại: {0}</p>", obj.Phone);
                    body.AppendFormat("<p>- Email: {0}</p>", obj.Email);
                    body.AppendFormat("<p>- Thời gian: {0}.{1}</p>", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Environment.NewLine);

                    body.AppendFormat("<p>Trân trọng.{0}</p>", Environment.NewLine);
                    body.AppendFormat("<p><i>Đây là Email gửi đăng ký tự động, vui lòng không reply. Xin cảm ơn!</i></p>");

                    MailMessage mail = new MailMessage();
                    mail.To.Add("hai38e5@gmail.com");
                    mail.CC.Add("edenrosehn@gmail.com");
                    mail.From = new MailAddress("edenrosehn@gmail.com");
                    mail.Body = body.ToString();
                    mail.Subject = "Thông tin khách hàng đăng ký dự án Edenrose";
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("edenrosehn@gmail.com", "eden@123"); // Enter seders User name and password  
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    OutputLog.WriteOutputLog(ex);
                }

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