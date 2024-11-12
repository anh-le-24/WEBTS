using System;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers
{
    public class TrangCaNhanController : Controller
    {
        // GET: TrangCaNhan
        DatabaseModel db = new DatabaseModel();
        public ActionResult Index(string idnd)
        {
            ViewBag.list = db.get("EXEC XemNguoiDungTheoID " + idnd);
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
                

        public ActionResult Donhang(string id)
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoIDND " + id);
            return View();
        }
        public ActionResult ChoXacNhan()
        {
            return View();
        }
        public ActionResult ĐangGiao()
        {
            return View();
        }
        public ActionResult DaGiao()
        {
            return View();
        }
        public ActionResult DaHuy()
        {
            return View();
        }

        public ActionResult Lichsudonhhang()
        {
            return View();
        }
        public ActionResult Voucher()
        {
            return View();
        }
        public ActionResult Diemtichluy()
        {
            return View();
        }
    }
}