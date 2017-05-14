using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using 2 thu vien thiet ke class metadata
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Online_Boutique.Models.MyModels
{
    [MetadataTypeAttribute(typeof(SanPhamMetadata))]
    public partial class SanPham
    {
        internal sealed class SanPhamMetadata
        {
            //[Required(ErrorMessage ="Vui lòng nhập dữ liệu cho trường này")]
            [Display(Name ="Mã")]
            public int masp { get; set; }

            [StringLength(15,ErrorMessage ="Độ dài tối đa 15 kí tự",MinimumLength =5)]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            [Display(Name = "Tên")]
            public string tensp { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            [Display(Name = "Giá Gốc")]
            public Nullable<int> giagocsp { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            [Display(Name = "Tên Loại")]
            public Nullable<int> maloaisp { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            [Display(Name = "Giá Bán")]
            public Nullable<int> giabansp { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            [Display(Name = "Ngày Nhập")]
            public Nullable<System.DateTime> ngaynhapsp { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            [Display(Name = "Số Lượng")]
            public Nullable<int> soluongsp { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            [Display(Name = "Mô Tả")]
            public string motasp { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            [Display(Name = "Ảnh")]
            public string anhsp { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            [Display(Name = "Tình Trạng")]
            public string tinhtrangsp { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            [Display(Name = "Size")]
            public string size { get; set; }

        }
    }
}