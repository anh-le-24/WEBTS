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
        DatabaseModel db = new DatabaseModel();
        public ActionResult Discount()
        {
            
            ViewBag.listVC = db.get("select * from MAGIAMGIA");
            return View();
        }
        [HttpPost]
        public ActionResult SaveCoup(string idvc) {
            
            ViewBag.list = db.get("EXEC LuuVoucherChoKhachHang " +idvc+ "," + Session["taikhoan"]);
            return RedirectToAction("Discount", "Sale");
        }



        public ActionResult MyVoucher() {
        
            ViewBag.list = db.get("EXEC XemVoucherDaLuu " + Session["taikhoan"] + ";");
            return View();
        }
    }
}