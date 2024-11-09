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