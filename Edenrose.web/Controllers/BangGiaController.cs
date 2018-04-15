﻿using Edenrose.Common.Enum;
using Edenrose.Data.EF;
using Edenrose.Data.Service;
using Edenrose.web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Vks.Common.Utils;

namespace Edenrose.web.Controllers
{
    public class BangGiaController : Controller
    {
        private readonly ResignService _resignService;
        private readonly ArticlesService _artilesService;
        private readonly ConfigService _configService;
        public BangGiaController()
        {
            _artilesService = new ArticlesService();
            _resignService = new ResignService();
            _configService = new ConfigService();
        }
        public ActionResult Index(int pageindex = 1)
        {
            var model = new ArticleViewModel();
            int totalItem = 0;
            model.ListData = _artilesService.GetData(pageindex, 10, out totalItem, (int)TypeArticle.BangGia);
            model.TotalItem = totalItem;
            model.PageIndex = pageindex;
            model.ListMoiNhat = _artilesService.GetDataNew((int)TypeArticle.BangGia);
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
            model.ListMoiNhat = _artilesService.GetDataNew((int)TypeArticle.BangGia);
            //model.ListCungChuDe = _artilesService.GetDataCungChuDe();
            return View(model);
        }


        [HttpPost]
        public bool Resign(string email, string name, string phone)
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

                //gửi email cho khách hàng

                try
                {
                    var noiDungEmail = _configService.GetbyKey("EmailContain").Value;
                    MailMessage mail = new MailMessage();
                    mail.To.Add(obj.Email);
                    mail.From = new MailAddress("bdslienke@gmail.com");
                    mail.Body = noiDungEmail;
                    mail.Subject = "Thông tin dự án ATHENA FULLAND";
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("bdslienke@gmail.com", "hanoi123456"); // Enter seders User name and password  
                    smtp.EnableSsl = true;

                    var filePath = System.Web.HttpContext.Current.Request.MapPath(string.Format("/BangGia"));
                    foreach (var files in Directory.GetFiles(filePath))
                    {
                        //FileInfo info = new FileInfo(files);
                        //var fileName = Path.GetFileName(info.FullName);
                        Attachment attachment;
                        attachment = new Attachment(files);
                        mail.Attachments.Add(attachment);
                    }
                    smtp.Send(mail);
                    return true;
                }
                catch (Exception ex)
                {
                    OutputLog.WriteOutputLog(ex);
                }
                return false;
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw;
            }
        }
    }
}