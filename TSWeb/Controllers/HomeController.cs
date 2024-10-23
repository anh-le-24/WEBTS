using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers
{
    public class HomeController : Controller
    {
        DatabaseModel db = new DatabaseModel();
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Chitiet()
        {
            return View();
        }
        public ActionResult TimkiemSp(string tensp)
        {
            ViewBag.list = db.get("EXEC TIMKIEMSACHTHEOTEN '" + tensp + "'");
            return View();
        }

        public ActionResult vd()
        {
            DatabaseModel db = new DatabaseModel();
            ViewBag.list = db.get("select * from SANPHAM");
            return View();
        }
        public ActionResult ThanhToan()
        {
            return View();
        }
    }
}