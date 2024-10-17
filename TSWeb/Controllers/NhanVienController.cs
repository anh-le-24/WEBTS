using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly DatabaseModel _databaseModel;

        public NhanVienController()
        {
            _databaseModel = new DatabaseModel();
        }
        public ActionResult DNNV()
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            bool isAuthenticated = _databaseModel.Login(username, password);

            if (isAuthenticated)
            {
                // Nếu đăng nhập thành công, chuyển hướng đến trang chính
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Nếu thất bại, quay lại trang đăng nhập với thông báo lỗi
                ViewBag.ErrorMessage = "Sai tên đăng nhập hoặc mật khẩu.";
                return View();
            }
        }

    }
}
