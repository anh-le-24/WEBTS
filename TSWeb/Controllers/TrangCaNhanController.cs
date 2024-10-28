using System;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers
{
    public class TrangCaNhanController : Controller
    {
        // GET: TrangCaNhan
        DatabaseModel db = new DatabaseModel();
        public ActionResult TrangCaNhan()
        {
            ViewBag.list = db.get("EXEC XemNguoiDungTheoID 1;");
            return View();
        }
        public ActionResult Donhang()
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