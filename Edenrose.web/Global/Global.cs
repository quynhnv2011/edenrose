using Edenrose.Data.Object;
using Edenrose.Data.Service;
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
        public static string Logo
        {
            get
            {
                return (new ConfigService()).GetbyKey("logo").Value;
            }
        }

        public static string Title
        {
            get
            {
                return (new ConfigService()).GetbyKey("title") != null ? (new ConfigService()).GetbyKey("title").Value : string.Empty;
            }
        }
        public static string Description
        {
            get
            {
                return (new ConfigService()).GetbyKey("description") != null ? (new ConfigService()).GetbyKey("description").Value: string.Empty;
            }
        }
    }

   
}