using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSWeb.Models {
    public class CartItem {
        public int IDSP { get; set; }      // ID của sản phẩm
        public string TenSP { get; set; }  // Tên sản phẩm
        public int SoLuong { get; set; }        // Số lượng sản phẩm
        public decimal Gia { get; set; }       // Giá sản phẩm
        public string HinhAnh { get; set; }
    }
}