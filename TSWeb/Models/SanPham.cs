using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSWeb.Models {
    public class SanPham {
        [Key]
        public int IDSP { get; set; }

        public string TenSP { get; set; }
        public decimal Gia { get; set; }
        public string HinhAnh { get; set; }
        public DateTime NgayTao { get; set; }
        public string MoTa { get; set; }
        public decimal GiamGia { get; set; }

    }
}