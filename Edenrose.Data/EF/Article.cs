//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Edenrose.Data.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        public int id { get; set; }
        public string TitleHome { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string ContentDetail { get; set; }
        public string Picture { get; set; }
        public string Url { get; set; }
        public Nullable<int> TypeArticle { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> Visits { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<bool> IsShow { get; set; }
    }
}
