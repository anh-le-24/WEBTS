using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSWeb.Models
{
    public class DangNhapDKModels
    {
<<<<<<< HEAD

        [Key]
        public int IDND { get; set; }

        [Required]
        public string TenND { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string MatKhau { get; set; }

        [Required]
        public long Sdt { get; set; }

        [Required]
=======
        [Key]
        public int IDND { get; set; }

        [Required(ErrorMessage = "Tên người dùng là bắt buộc")]
        [StringLength(30, ErrorMessage = "Tên người dùng không được quá 30 ký tự")]
        public string TenND { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email phải có định dạng @gmail.com")]
        [StringLength(50, ErrorMessage = "Email không được quá 50 ký tự")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(20, ErrorMessage = "Mật khẩu phải dài hơn 12 ký tự", MinimumLength = 12)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$",
            ErrorMessage = "Mật khẩu phải chứa ít nhất một ký tự đặc biệt, một chữ viết hoa, một chữ viết thường và một số")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Số điện thoại phải có từ 10 đến 11 số")]
        public long Sdt { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
>>>>>>> vanh
        public string Diachi { get; set; }

        [Required]
        public DateTime NgayTaoTK { get; set; } = DateTime.Now;  // Ngày tạo mặc định là ngày hôm nay

        [Required]
        public int IDVT { get; set; }  // Foreign Key to VAITRO
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> vanh
