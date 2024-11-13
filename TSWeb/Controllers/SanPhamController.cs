using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSWeb.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThemSanPham()
        {
            return View();  // Trả về view ThemSanPham.cshtml
        }

        public ActionResult ChinhSuaSanPham()
        {
            return View();
        }

        public ActionResult XoaSanPham()
        {
            return View();
        }
    }
}