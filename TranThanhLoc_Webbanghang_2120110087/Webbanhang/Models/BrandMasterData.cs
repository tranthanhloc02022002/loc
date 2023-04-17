using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Webbanhang.Models
{
    public partial class BrandMasterData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nhập tên sản phẩm")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Hình Đại diện")]
        public string Avartar { get; set; }
        [Display(Name = "Đường dẫn")]
        public string Slug { get; set; }
        [Display(Name = "HiệnTrênTrang Chủ")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        [Display(Name = "Thứ tự hiển thị")]
        public Nullable<int> DisplayOrder { get; set; }
        [Display(Name = "Đã xóa")]
        public Nullable<bool> Deleted { get; set; }
        

    }
}