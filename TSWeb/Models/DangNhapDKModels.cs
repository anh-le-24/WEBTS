using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSWeb.Models
{
    public class DangNhapDKModels
    {

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
        public string Diachi { get; set; }

        [Required]
        public DateTime NgayTaoTK { get; set; } = DateTime.Now;  // Ngày tạo mặc định là ngày hôm nay

        [Required]
        public int IDVT { get; set; }  // Foreign Key to VAITRO
    }
}
