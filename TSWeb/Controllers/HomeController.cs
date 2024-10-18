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
        public ActionResult vd()
        {
            DatabaseModels db = new DatabaseModels();
            ViewBag.list = db.GetData("select * from SANPHAM");
            return View();
        } 
    }
}