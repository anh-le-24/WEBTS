using System;
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
        public ActionResult DoiMK(string currentPassword,string newPassword)
        {
            ViewBag.list = db.get("EXEC DoiMatKhau " + Session["taikhoan"] + ", '" + currentPassword + "', '" + newPassword + "';");
            return RedirectToAction("Index", "Home");              
        }
                

        public ActionResult Donhang()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoIDND " + Session["taikhoan"]);
            return View();
        }
        public ActionResult ChoXacNhan()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoNguoiDung "+ Session["taikhoan"] +",N'Đang xử lý';");
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
        public ActionResult DelVch() {
            ViewBag.list = db.get("");
            return View();
        }


        public ActionResult Diemtichluy()
        {
            return View();
        }
    }
}