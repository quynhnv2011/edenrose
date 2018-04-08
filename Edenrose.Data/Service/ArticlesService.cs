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
        public List<Article> GetData(int pageIndex, int pageSize, out int totalItem)
        {
            try
            {
              
                var lstData = _context.Articles.Where(p => p.TypeArticle == (int)TypeArticle.TinTuc && p.Deleted != true);
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
        public Article GetById(int id, int key = 0)
        {
            try
            {
                var model = _context.Articles.Find(id);
                if(key > 0)
                {
                    if (model == null) model = new Article();
                    model.ListPicture = _context.Pictures.Where(x => x.Key == key && x.ReferenceId == model.id).ToList();
                }             
                return model;
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }

        public Article GetByUrl(string url)
        {
            try
            {
                return _context.Articles.Where(x=>x.Url.Trim().ToLower() == url.Trim().ToLower()).First();
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                return new Article();
                throw ex;
            }
        }
        public bool Add(Article entity)
        {
            try
            {
                _context.Articles.Add(entity);
                _context.SaveChanges();
                if (entity.ListPicture != null && entity.ListPicture.Count > 0)
                {
                    entity.ListPicture.ForEach(x => x.ReferenceId = entity.id);
                    _context.Pictures.AddRange(entity.ListPicture);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
        public bool Update(Article entity, int key = 0)
        {
            try
            {
                var objOld = GetById(entity.id);
                if (objOld.id > 0)
                {
                    ObjectUtils.CopyObject(entity, ref objOld, true);
                    //_context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    if (entity.ListPicture != null && entity.ListPicture.Count > 0 && key > 0)
                    {
                        var ListOldPicture = _context.Pictures.Where(x => x.Key == key && x.ReferenceId == entity.id).ToList();
                        _context.Pictures.RemoveRange(ListOldPicture);
                        _context.Pictures.AddRange(entity.ListPicture);
                    }
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

        public Article GetByKey(int type)
        {
            try
            {
                return _context.Articles.Where(x => x.TypeArticle == type).First();
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                return new Article();
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
                var result = lstData.OrderBy(m => m.DisplayOrder).ToList();
                foreach(var item in result)
                {
                    item.ListPicture = _context.Pictures.Where(x => x.Key == (int)TypeTopic.Product && x.ReferenceId == item.id).ToList();
                }
                return result;
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

        public List<Article> GetDataNew()
        {
            try
            {
                var lstData = _context.Articles.Where(x => x.TypeArticle == (int)TypeArticle.TinTuc && x.Deleted != true && x.IsShow == true);
                return lstData.OrderByDescending(m => m.CreatedDate).Take(10).ToList();
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
        public List<Article> GetDataCungChuDe()
        {
            try
            {
                var lstData = _context.Articles.Where(x => x.TypeArticle == (int)TypeArticle.TinTuc && x.Deleted != true && x.IsShow == true);
                return lstData.OrderByDescending(m => m.Visits).Take(6).ToList();
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                throw ex;
            }
        }
    }
}
