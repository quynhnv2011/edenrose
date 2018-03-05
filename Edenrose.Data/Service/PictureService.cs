using Edenrose.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edenrose.Data.Service
{
    public class PictureService : BaseService
    {
        public PictureService()
            : base()
        {

        }

        public Picture GetByKey(int key)
        {
            try
            {
                var model = _context.Pictures.Where(x => x.Key == key).First();
              
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateMatBang(List<Picture> List)
        {
            try
            {
                foreach(var item in List)
                {
                    if(item.id > 0)
                    {
                        var obj = _context.Pictures.Find(item.id);
                        if (obj != null)
                            obj.Url = item.Url;
                    }
                    else
                    {
                        _context.Pictures.Add(item);
                    }
                }
                _context.SaveChanges();              

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
