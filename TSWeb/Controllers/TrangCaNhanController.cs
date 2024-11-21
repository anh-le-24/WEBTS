using System;
using System.Data.SqlTypes;
using System.Net;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers
{
    public class TrangCaNhanController : Controller
    {
        // GET: TrangCaNhan
        DatabaseModel db = new DatabaseModel();
        public ActionResult Index()
        {
            ViewBag.list = db.get("EXEC XemNguoiDungTheoID " + Session["taikhoan"]);
            return View();
        }

        public ActionResult DoiMatKhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoiMK(string currentPassword, string newPassword)
        {
            try
            {
                ViewBag.list = db.get("EXEC DoiMatKhau " + Session["taikhoan"] + ", '" + currentPassword + "', '" + newPassword + "';");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ) {
                return RedirectToAction("DoiMatKhau", "TrangCaNhan");
            }
        }
            
        public ActionResult Donhang()
        {
            ViewBag.dg = db.get("select * from DANHGIA");
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoIDND " + Session["taikhoan"]);
            return View();
        }
        public ActionResult XemChiTietDH(string id)
        {
            ViewBag.tt = db.get("EXEC XemThongTinThanhToan "+ id);
            ViewBag.ct = db.get("EXEC XemChiTietDonHang " + id);
            return View();
        }
        public ActionResult ChoXacNhan()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoNguoiDung " + Session["taikhoan"] + ",N'Đang xử lý';");
            return View();
        }
        public ActionResult DaXacNhan()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoNguoiDung " + Session["taikhoan"] + ",N'Đã xác nhận';");
            return View();
        }

        public ActionResult DangGiao()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoNguoiDung " + Session["taikhoan"] + ",N'Đang giao';");
            return View();
        }
        public ActionResult DaGiao()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoNguoiDung " + Session["taikhoan"] + ",N'Đã hoàn thành';");
            return View();
        }
        public ActionResult DaHuy()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoNguoiDung " + Session["taikhoan"] + ",N'Bị hủy';");
            return View();
        }

        public ActionResult Lichsudonhhang()
        {
            return View();
        }
        public ActionResult Voucher()
        {
            ViewBag.list = db.get("EXEC XemVoucherDaLuu " + Session["taikhoan"]);
            return View();
        }

        [HttpPost]
        public ActionResult DelVch()
        {
            ViewBag.list = db.get("");
            return View();
        }


        public ActionResult Diemtichluy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CapNhatTK(string name,
            string email,
            string phone,
            string address,
            int notification)
        {
            try
            {
                string sql = ("EXEC SuaNguoiDungKhachHang " + Session["taikhoan"] + ",N'" + name + "','" + email + "','" 
                    + phone + "',N'" + address + "'," + notification + ";");
                db.get(sql);
                return RedirectToAction("Index", "TrangCaNhan");
               
            }
            catch (Exception)
            {
                return RedirectToAction("Index","Home");
            }
            
        }
        public ActionResult XemThongBao() 
        {
            ViewBag.thongbaos = db.get("EXEC xemthongbao " + Session["taikhoan"]);
            return View(); 
        }

        [HttpPost]
        public ActionResult ThemDanhGia(string diem,
            string noiDung,
            string iddh)
        {
            DateTime date = DateTime.Today;  // Lấy ngày hôm nay mà không bao gồm thời gian
           
            try
            {
                db.get("EXEC ThemDanhGia '"+ diem +"'',N'"+ noiDung + "','"+ date +"',"+ Session["taikhoan"] +","+ iddh);
                return RedirectToAction("DonHang", "TrangCaNhan");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}