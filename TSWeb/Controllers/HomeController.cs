using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSWeb.Models;
using System.Configuration;
using System.Collections;
using System.CodeDom.Compiler;

namespace TSWeb.Controllers {
    public class HomeController : Controller {
        DatabaseModel db = new DatabaseModel();

        public ActionResult Index() {
            ViewBag.list = db.get("SELECT * FROM SANPHAM");
            return View();
        }
        public ActionResult Cart(string id) {
            ViewBag.list = db.get("EXEC XemTatCaSanPhamGioHang " + id + ";");
            return View();
        }

        public ActionResult Chitiet(string idsp) {
            ViewBag.list = db.get("EXEC HienThiSanPhamTheoID " + idsp + ";");
            return View();
        }
        public ActionResult vd() {
            ViewBag.list = db.get("select * from SANPHAM");
            return View();
        }
        public ActionResult ThanhToan() {
            return View();
        }
        public ActionResult ListSanPham() {
            return View();
        }


    }
}