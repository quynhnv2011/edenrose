using Edenrose.Data.Object;
using System.Web;


namespace Edenrose.web.Global
{
    public static class SessionInfo
    {
        public static Account CurrentUser
        {
            get
            {
                if (!HttpContext.Current.Request.IsAuthenticated)
                {
                    return null;
                }
                var json = HttpContext.Current.User.Identity.Name;
                var accInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<Account>(json);
                return accInfo;
            }
        }
    }
}