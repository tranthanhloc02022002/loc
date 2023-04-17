using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webbanhang.Models
{
    public class CategoryMasterData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nhập tên sản phẩm")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Hình Đại diện")]
        public string Avartar { get; set; }
        [Display(Name = "Đường dẫn")]
        public string Slug { get; set; }
        [Display(Name = "Hiện Trên Trang Chủ")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        [Display(Name = "Thứ tự hiển thị")]
        public Nullable<int> DisplayOrder { get; set; }
        [Display(Name = "Đã xoá")]
        public Nullable<bool> Deleted { get; set; }
        [Display(Name = "Thời gian tạo")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        [Display(Name = "Thời gian cập nhật")]
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
    }
}