using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSWeb.Controllers
{
    public class SanPhamController : Controller
    {
        public ActionResult Index()
        {
            return View();  // Trang Index của sản phẩm
        }

        public ActionResult ThemSanPham()
        {
            return View("ThemSanPham");  // Trả về view ThemSanPham.cshtml
        }

        public ActionResult ChinhSuaSanPham()
        {
            return View("ChinhSuaSanPham");
        }

        public ActionResult XoaSanPham()
        {
            return View("XoaSanPham");
        }
    }
}
