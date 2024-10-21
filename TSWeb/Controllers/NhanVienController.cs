using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers
{
    public class NhanVienController : Controller
    {
        public ActionResult DNNV()
        {
            return View(); 
        }
        public ActionResult HomeNV() 
        {
            DatabaseModel db = new DatabaseModel();
            ViewBag.list = db.get("select * from DONHANG");
            return View();
        }

    }
}
