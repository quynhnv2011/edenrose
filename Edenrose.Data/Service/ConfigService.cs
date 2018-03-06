using Edenrose.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vks.Common;
using Vks.Common.Utils;

namespace Edenrose.Data.Service
{
    public class ConfigService : BaseService
    {
        public ConfigService()
            : base()
        {

        }
        public List<Config> GetData(int pageIndex, int pageSize, out int totalItem)
        {
            try
            {
                var lstData = _context.Configs.Where(x => x.id > 0);
                totalItem = lstData.Count();
                lstData = lstData.OrderBy(x => x.Name);
                if (totalItem > ((pageIndex - 1) * pageSize))
                    lstData = lstData.Skip((pageIndex - 1) * pageSize).Take(pageSize);

                return lstData.ToList();
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
        public Config GetbyKey(string key)
        {
            try
            {
                return _context.Configs.SingleOrDefault(x => x.Name.Trim().ToLower() == key.Trim().ToLower());
            }
            catch
            {
                throw;
            }
        }
        public Config GetbyId(int id)
        {
            try
            {
                return _context.Configs.Find(id);
            }
            catch
            {
                throw;
            }
        }
        public bool Add(Config entity)
        {
            try
            {
                _context.Configs.Add(entity);
               
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
        public bool Update(Config entity)
        {
            try
            {
                var objOld = GetbyId(entity.id);
                if (objOld.id > 0)
                {
                    ObjectUtils.CopyObject(entity, ref objOld, true);                    
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
    }
}
