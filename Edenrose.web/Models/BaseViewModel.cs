using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Edenrose.web.Models
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            IsNotPaging = false;
        }

        public bool IsNotPaging { get; set; }
        public int PageIndex
        {
            get;set;
        }
        public int PageSize
        {
            get
            {
                return 10;
            }
            set { }
        }
        public int TotalItem { get; set; }
      
        public string GetHtmlPaging
        {
            get
            {
                var str = new StringBuilder();
               
                str.AppendFormat("<span class=\"pages\">Trang {0} trên {1}</span>", PageIndex, TotalItem / PageSize);
                if (TotalItem <= PageSize)
                {
                    return string.Empty;
                }

                if (PageSize == 0 || TotalItem == 0)
                {
                    return string.Empty;
                }
                var totalPage = TotalItem / PageSize;
                var dongdu = TotalItem % PageSize;
                if (dongdu != 0)
                {
                    totalPage = totalPage + 1;
                }

                if (totalPage == 1)
                {
                    return string.Empty;
                }

                for (int i = 1; i <= totalPage; i++)
                {
                    if (i == PageIndex)
                    {
                        str.AppendFormat("<span class=\"current\">{0}</span>", i);
                    }
                    else
                    {
                        if (i >= PageIndex - 3 && i <= PageIndex + 3)
                        {
                            str.AppendFormat("<a href='{0}' class=\"page larger\" onclick=\"_global.unBlockUI('#content')\">{1}</a>", string.Format("/bang-gia/page/{0}", i), i);
                        }
                    }
                }

                if (PageIndex != totalPage)
                {
                    str.AppendFormat("<a class=\"nextpostslink\" rel=\"next\" href=\"{0}\">»</a>", string.Format("/bang-gia/page/{0}", PageIndex + 1));
                }
                return str.ToString();
            }
        }
    }
}