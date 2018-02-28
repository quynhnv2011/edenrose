using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vks.Common.Utils;

namespace Edenrose.web.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected string Provider = "entityframework";
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (!requestContext.HttpContext.Request.IsAuthenticated)
            {
                if (!requestContext.HttpContext.Response.IsRequestBeingRedirected)
                    requestContext.HttpContext.Response.Redirect(GetLoginUrl(requestContext));
            }
            base.Initialize(requestContext);
        }
        private string GetLoginUrl(System.Web.Routing.RequestContext requestContext)
        {
            var redirectUrl = requestContext.HttpContext.Server.UrlEncode(requestContext.HttpContext.Request.Url.PathAndQuery);
            return string.Format("/Admin/Account/DoLogin?RedirectUrl={0}", redirectUrl);
        }

        /// <summary>
        /// Render with signle view
        /// </summary>
        /// <param name="partialViewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual string RenderPartialView(string partialViewName, object model)
        {
            if (ControllerContext == null)
                return string.Empty;

            if (model == null)
                throw new ArgumentNullException("model");

            if (string.IsNullOrEmpty(partialViewName))
                throw new ArgumentNullException("partialViewName");

            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, partialViewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        public virtual object GetBaseObjectResult(bool isSuccess = true, string msg = "")
        {
            return new
            {
                IsSuccess = isSuccess,
                Message = msg
            };
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //Log Exception e
            OutputLog.WriteOutputLog(filterContext.Exception);
        }
    }
}