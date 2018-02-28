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
    public class TopicService : BaseService
    {
        public TopicService()
            : base()
        {

        }

        public Topic GetByKey(int key)
        {
            try
            {
                return _context.Topics.SingleOrDefault(x => x.key == key);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public Topic GetById(int id)
        {
            try
            {
                return _context.Topics.Find(id);
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
        public bool Add(Topic entity)
        {
            try
            {
                _context.Topics.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
        public bool Update(Topic entity)
        {
            try
            {
                var objOld = GetById(entity.id);
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
