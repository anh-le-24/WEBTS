using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSWeb.Controllers
{
    public class SanPhamController : Controller
    {
        public ActionResult Index()
        {
            return View();  // Trang Index của sản phẩm
        }

        public ActionResult ThemSanPham()
        {
            return View("ThemSanPham");  // Trả về view ThemSanPham.cshtml
        }

        public ActionResult ChinhSuaSanPham()
        {
            return View("ChinhSuaSanPham");
        }

        public ActionResult XoaSanPham()
        {
            return View("XoaSanPham");
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Chitiet()
        {
            return View();
        }
        public ActionResult Payment()
        {
            return View();
        }
        public ActionResult TongQuan()
        {
            return View();
        }
        public ActionResult QLSanPham()
        {
            return View();
        }
        public ActionResult ThanhToan()
        {
            return View();
        }
        public ActionResult UuDai()
        {
            return View();
        }
        public ActionResult ChinhSuaSP()
        {
            return View();
        }
        public ActionResult DanhSachSP()
        {
            return View();
        }
        public ActionResult Topping()
        {
            return View();
        }
       
        public ActionResult ThemTopping()
        {
            return View();
        }
        public ActionResult ChinhSuaTopping()
        {
            return View();
        }
        // Các phương thức quản lý khách hàng
        public ActionResult QLKhachHang()
        {
            return View();
        }
        public ActionResult ThemKhachHang()
        {
            return View("ThemKhachHang"); // Trả về view để thêm khách hàng
        }

        public ActionResult ChinhSuaKhachHang()
        {
            return View("ChinhSuaKhachHang"); // Trả về view để chỉnh sửa khách hàng
        }

        public ActionResult XoaKhachHang()
        {
            return View("XoaKhachHang"); // Trả về view để xóa khách hàng
        }
        // Các phương thức quản lý nhân viên
        public ActionResult QLNhanVien()
        {
            return View();
        }
        public ActionResult ThemNV()
        {
            return View("ThemNV");
        }
        public ActionResult ChinhSuaNV()
        {
            return View("ChinhSuaNV");
        }
    }
}
