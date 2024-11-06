using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers 
{
    public class SaleController : Controller
    {
        // GET: Sale
        public ActionResult Discount()
        {
            DatabaseModel db = new DatabaseModel();
            ViewBag.listVC = db.get("select * from MAGIAMGIA");
            return View();
        }
        [HttpPost]
        public ActionResult SaveCoup(string tenma, string mota, string ngaybd, string ngaykt, string phantramgiam, string dieukien) {
            DatabaseModel db = new DatabaseModel();
            db.get("EXEC LuuMaGiamGia N'" +tenma+ "', N'" +mota+ "','" +ngaybd+ "','" +ngaykt+ "'," +phantramgiam+ "," +dieukien+ ";");
            return RedirectToAction("Discount", "Sale");
        }
        public ActionResult MyVoucher() {
            DatabaseModel db = new DatabaseModel();
            ViewBag.listMYVC = db.get("select * from MYVOUCHER");
            return View();
        }
    }
}