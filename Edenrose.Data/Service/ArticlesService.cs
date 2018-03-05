using Edenrose.Common.Enum;
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
    public class ArticlesService: BaseService
    {
        public ArticlesService()
            : base()
        {

        }

        public List<Article> GetData(string title, int pageIndex, int pageSize, out int totalItem, int TypeArticle = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(title)) title = string.Empty;
                var lstData = _context.Articles.Where(p => p.Title.Contains(title) && p.Deleted != true);
                if(TypeArticle > 0)
                {
                    lstData = lstData.Where(x => x.TypeArticle == TypeArticle);
                }
                totalItem = lstData.Count();
                lstData = lstData.OrderBy(t => t.CreatedDate);
                if (totalItem > ((pageIndex - 1) * pageSize))
                    lstData = lstData.Skip((pageIndex - 1) * pageSize).Take(pageSize);

                return lstData.OrderBy(m => m.DisplayOrder).ToList();
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
        public Article GetById( int id)
        {
            try
            {
                return _context.Articles.Find(id);
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
        public bool Add(Article entity)
        {
            try
            {
                _context.Articles.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
        public bool Update(Article entity)
        {
            try
            {
                var objOld = GetById(entity.id);
                if (objOld.id > 0)
                {
                    ObjectUtils.CopyObject(entity, ref objOld, true);
                    //_context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
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

        public List<Article> GetDataTongQuan()
        {
            try
            {
                var lstData = _context.Articles.Where(x => x.TypeArticle == (int)TypeArticle.TongQuan && x.Deleted != true && x.IsShow == true);
                return lstData.OrderBy(m => m.DisplayOrder).Take(2).ToList();
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
        public List<Article> GetDataSanPham()
        {
            try
            {
                var lstData = _context.Articles.Where(x => x.TypeArticle == (int)TypeArticle.SanPham && x.Deleted != true && x.IsShow == true);
                return lstData.OrderBy(m => m.DisplayOrder).ToList();
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
        public List<Article> GetDataTopArticle()
        {
            try
            {
                var lstData = _context.Articles.Where(x => x.TypeArticle == (int)TypeArticle.TinTuc && x.Deleted != true && x.IsShow == true);
                return lstData.OrderByDescending(m => m.CreatedDate).Take(3).ToList();
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
    }
}
