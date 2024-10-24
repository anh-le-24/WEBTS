using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QLKhachHang()
        {
            return View();
        }
        public ActionResult ThemKhachHang()
        {
            return View(); 
        }

        public ActionResult ChinhSuaKhachHang()
        {
            return View();
        }

        public ActionResult XoaKhachHang()
        {
            return View(); 
        }
        // Các phương thức quản lý nhân viên
        public ActionResult QLNhanVien()
        {
            return View();
        }
        public ActionResult ThemNV()
        {
            return View();
        }
        public ActionResult ChinhSuaNV()
        {
            return View();
        }
        public ActionResult XoaNV()
        {
            return View();
        }
    }
}