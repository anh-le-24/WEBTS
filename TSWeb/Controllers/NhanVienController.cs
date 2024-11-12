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
        public ActionResult DNNV()
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult XNDNNV(string email,string password)
        {
            db.get("EXEC KiemTraDangNhapNV '" + email + "','" + password + "';");

            return RedirectToAction("HomeNV","NhanVien");
        }
        public ActionResult HomeNV() 
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangKH");
            return View();
        }
        public ActionResult ThemDH()
        {
            ViewBag.list = db.get("select * from SANPHAM");
            ViewBag.Tp = db.get("select * from TOPPING");
            ViewBag.ctdh = db.get("select * from CTDONHANG");
            return View();
        }

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

        [HttpPost]
        public ActionResult XN(string iddh,string trangthai)
        {
            db.get("EXEC CapNhatTrangThaiDonHang " + iddh + ",N'" + trangthai + "';");
            return RedirectToAction("HomeNV");
        }

        public ActionResult BaoCaoLoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GuiBC(string tieude,string noidung) 
        {
            db.get(" EXEC AddNotificationForRole N'" + tieude + "', N'" + noidung + "';");
            return RedirectToAction("BaoCaoLoi","NhanVien"); 
        }

    }
}
