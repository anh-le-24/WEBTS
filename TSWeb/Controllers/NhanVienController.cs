using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers
{
    public class NhanVienController : Controller
    {
        DatabaseModel db = new DatabaseModel();

        // View cho Đăng nhập
        public ActionResult DNNV()
        {
            return View();
        }

        // API xác nhận đăng nhập
        [HttpPost]
        public JsonResult XNDNNVApi(string email, string password)
        {
            var result = db.get("EXEC KiemTraDangNhapNV '" + email + "','" + password + "';");
            bool isSuccess = result != null && result.Count > 0; 

            return Json(new { success = isSuccess }, JsonRequestBehavior.AllowGet);
        }

        // Action xác nhận đăng nhập, chuyển hướng đến Home
        [HttpPost]
        public ActionResult XNDNNV(string email, string password)
        {
            db.get("EXEC KiemTraDangNhapNV '" + email + "','" + password + "';");
            return RedirectToAction("HomeNV", "NhanVien");
        }

        // View cho Home
        public ActionResult HomeNV()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangKH");
            return View();
        }

        // API cho Home
        [HttpGet]
        public JsonResult HomeNVApi()
        {
            var orders = db.get("EXEC XemTatCaDonHangKH");
            return Json(new { data = orders }, JsonRequestBehavior.AllowGet);
        }

        // View cho thêm đơn hàng
        public ActionResult ThemDH()
        {
            ViewBag.list = db.get("select * from SANPHAM");
            ViewBag.Tp = db.get("select * from TOPPING");
            ViewBag.ctdh = db.get("select * from CTDONHANG");
            return View();
        }

        // Các view trạng thái đơn hàng
        public ActionResult DonchoXN()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoTrangThai N'Đang Xử lý';");
            return View();
        }

        public ActionResult DonDaXN()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoTrangThai N'Đã xác nhận';");
            return View();
        }

        public ActionResult DonDangGiao()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoTrangThai N'Đang giao';");
            return View();
        }

        public ActionResult DonDaHT()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoTrangThai N'Đã hoàn thành';");
            return View();
        }

        public ActionResult DonBiHuy()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoTrangThai N'Bị hủy';");
            return View();
        }

        // API cập nhật trạng thái đơn hàng
        [HttpPost]
        public JsonResult XNApi(string iddh, string trangthai)
        {
            db.get("EXEC CapNhatTrangThaiDonHang " + iddh + ",N'" + trangthai + "';");
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // Action cập nhật trạng thái đơn hàng
        [HttpPost]
        public ActionResult XN(string iddh, string trangthai)
        {
            db.get("EXEC CapNhatTrangThaiDonHang " + iddh + ",N'" + trangthai + "';");
            return RedirectToAction("HomeNV");
        }

        // View cho báo cáo lỗi
        public ActionResult BaoCaoLoi()
        {
            return View();
        }

        // API gửi báo cáo lỗi
        [HttpPost]
        public JsonResult GuiBCApi(string tieude, string noidung)
        {
            db.get(" EXEC AddNotificationForRole N'" + tieude + "', N'" + noidung + "';");
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // Action gửi báo cáo lỗi
        [HttpPost]
        public ActionResult GuiBC(string tieude, string noidung)
        {
            db.get(" EXEC AddNotificationForRole N'" + tieude + "', N'" + noidung + "';");
            return RedirectToAction("BaoCaoLoi", "NhanVien");
        }
    }
}
