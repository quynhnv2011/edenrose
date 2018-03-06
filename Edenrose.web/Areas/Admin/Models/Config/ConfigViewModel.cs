using Edenrose.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edenrose.web.Areas.Admin.Models
{
    public class ConfigViewModel: BaseListModel
    {
        public List<Config> ListData { get; set; }
    }
}