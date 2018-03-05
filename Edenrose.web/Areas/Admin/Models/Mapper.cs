using Edenrose.Common;
using Edenrose.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edenrose.web.Areas.Admin.Models
{
    public static class Mapper
    {
        #region Tổng quan
        public static Article ToModel(this TongQuanItemModel entity)
        {
            return new Article
            {
                id = entity.id,
                Title = entity.Title,
                TitleHome = entity.TitleHome,
                ShortDescription = entity.ShortDescription,
                ContentDetail = entity.ContentDetail,
                Picture = entity.Picture,
                Url = entity.Url,
                TypeArticle = entity.TypeArticle,
                SeoTitle = entity.SeoTitle,
                SeoDescription = entity.SeoDescription,
                SeoKeywords = entity.SeoKeywords,
                Visits = entity.Visits,
                DisplayOrder = entity.DisplayOrder,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                IsShow = entity.IsShow
                
         
            };
        }

        public static TongQuanItemModel ToModel(this Article entity)
        {
            return new TongQuanItemModel
            {
                id = entity.id,
                Title = entity.Title,
                TitleHome = entity.TitleHome,
                ShortDescription = entity.ShortDescription,
                ContentDetail = entity.ContentDetail,
                Picture = entity.Picture,
                Url = entity.Url,
                TypeArticle = entity.TypeArticle.AsInt(),
                SeoTitle = entity.SeoTitle,
                SeoDescription = entity.SeoDescription,
                SeoKeywords = entity.SeoKeywords,
                DisplayOrder = entity.DisplayOrder,
                Visits = entity.Visits,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                IsShow = entity.IsShow == true ? true : false
            };
        }
        #endregion Bài viết

        #region Tổng quan
        public static Article ToModel(this ArticleItemModel entity)
        {
            return new Article
            {
                id = entity.id,
                Title = entity.Title,
                TitleHome = entity.TitleHome,
                ShortDescription = entity.ShortDescription,
                ContentDetail = entity.ContentDetail,
                Picture = entity.Picture,
                Url = entity.Url,
                TypeArticle = entity.TypeArticle,
                SeoTitle = entity.SeoTitle,
                SeoDescription = entity.SeoDescription,
                SeoKeywords = entity.SeoKeywords,
                Visits = entity.Visits,
                DisplayOrder = entity.DisplayOrder,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                IsShow = entity.IsShow


            };
        }

        public static ArticleItemModel ToModelArticle(this Article entity)
        {
            return new ArticleItemModel
            {
                id = entity.id,
                Title = entity.Title,
                TitleHome = entity.TitleHome,
                ShortDescription = entity.ShortDescription,
                ContentDetail = entity.ContentDetail,
                Picture = entity.Picture,
                Url = entity.Url,
                TypeArticle = entity.TypeArticle.AsInt(),
                SeoTitle = entity.SeoTitle,
                SeoDescription = entity.SeoDescription,
                SeoKeywords = entity.SeoKeywords,
                DisplayOrder = entity.DisplayOrder,
                Visits = entity.Visits,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                IsShow = entity.IsShow == true ? true: false
            };
        }
        #endregion 
    }
}