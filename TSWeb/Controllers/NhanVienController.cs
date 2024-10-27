using System;
using System.Collections.Generic;
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
        public ActionResult HomeNV() 
        {
            ViewBag.list = db.get("select * from DONHANG");
            return View();
        }
        public ActionResult ThemDH()
        {
            ViewBag.list = db.get("select * from SANPHAM");
            ViewBag.Tp = db.get("select * from TOPPING");
            ViewBag.ctdh = db.get("select * from CTDONHANG");
            return View();
        }


    }
}
