using Edenrose.Common;
using Edenrose.Data.Service;
using Edenrose.web.Areas.Admin.Account;
using Edenrose.Data.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Edenrose.web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        public AccountController()
        {
            _accountService = new AccountService();
        }
        [HttpGet]
        public ActionResult DoLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult DoLogin(LoginModel m, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(m.RedirectUrl) || (string.IsNullOrEmpty(m.RedirectUrl) && (!string.IsNullOrEmpty(m.Email) || !string.IsNullOrEmpty(m.Password))))
                {
                    if (string.IsNullOrEmpty(m.Email) || string.IsNullOrEmpty(m.Password))
                    {
                        m.Password = string.Empty;
                        m.ErrMess = Vks.Common.Utils.MessageUtils.Err("Anh/chị chưa nhập đầy đủ thông tin đăng nhập");
                    }
                    else
                    {
                        var acc = new Edenrose.Data.Object.Account();
                        var isOk = _accountService.DoLogin(m.Email, Encrypt.EncryptMD5(m.Password), ref acc);
                        if (isOk)
                        {

                            m.Password = string.Empty;
                            acc.Password = string.Empty;
                            var json = Newtonsoft.Json.JsonConvert.SerializeObject(acc);
                            FormsAuthentication.SetAuthCookie(json, true);

                            if (string.IsNullOrEmpty(m.RedirectUrl))
                            {
                                return RedirectToAction("Index", "Home", new { Area = "Admin" });
                            }
                            else
                            {
                                return Redirect(Server.UrlDecode(m.RedirectUrl));
                            }
                        }
                        else
                        {
                            m.Password = string.Empty;
                            m.ErrMess = Vks.Common.Utils.MessageUtils.Err("Tên đăng nhập hoặc mật khẩu không chính xác");
                        }
                    }
                }

            }
            return View("DoLogin", m);

        }

        public ActionResult DoLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("DoLogin", "Account");
        }
    }
}