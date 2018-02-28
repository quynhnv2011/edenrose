using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Edenrose.web.Areas.Admin.Models
{
    public class TongQuanItemModel: BaseModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập tiêu đề hiển thị trên trang chủ")]
        [MaxLength(300, ErrorMessage = "Độ dài tối đa là 300 ký tự")]
        public string TitleHome { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập tiêu đề bài viết")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập mô tả ngắn")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập nội dung bài viết")]
        public string ContentDetail { get; set; }
        [Required(ErrorMessage = "Mời bạn chọn ảnh đại diện cho bài viết")]
        public string Picture { get; set; }
        public string Url { get; set; }
        public int TypeArticle { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> Visits { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> Deleted { get; set; }
    }
}