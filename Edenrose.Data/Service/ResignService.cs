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
    public class ResignService : BaseService
    {
        public ResignService()
            : base()
        {

        }

        public List<Resignation> GetData(int pageIndex, int pageSize, out int totalItem)
        {
            try
            {
                var lstData = _context.Resignations.Where(x=>x.Id > 0); 
                totalItem = lstData.Count();
                lstData = lstData.OrderBy(t => t.CreatedDate);
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
        public Resignation GetById(int id)
        {
            try
            {
                return _context.Resignations.Find(id);
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }

       
        public bool Add(Resignation entity)
        {
            try
            {
                _context.Resignations.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
    }
}
