using System;
using System.Collections;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using TSWeb.Models;
using Owin;
using Microsoft.Owin;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;
using System.Collections.Generic;

namespace TSWeb.Controllers

{
    public class DangNhapDKController : Controller
    {
        private readonly DatabaseModel db = new DatabaseModel();

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(string hoten, string email, string matkhau, string sdt, string diachi)
        {
            try
            {
                db.get($"EXEC ThemNguoiDung N'{hoten}', '{email}', '{matkhau}', {sdt}, '{diachi}';");
                TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Lỗi đăng ký: " + ex.Message);
                TempData["ErrorMessage"] = "Đăng ký thất bại! Vui lòng thử lại.";
            }
            return RedirectToAction("DangNhap", "DangNhapDK");
        }

        [HttpPost]
        public ActionResult XuLyDangNhap(string email, string matkhau)
        {
            try
            {
                var userList = db.get($"EXEC DangNhapNG '{email}', '{matkhau}'");

                if (userList != null && userList.Count > 0)
                {
                    var user = userList[0] as ArrayList;
                    if (user != null && user.Count >= 3)
                    {
                        if (int.TryParse(user[0]?.ToString(), out int idnd))
                        {
                            Session["taikhoan"] = idnd;
                            Session["tennguoidung"] = user[2]?.ToString();
                            Session["khachhang"] = user[1]?.ToString();
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                TempData["ErrorMessage"] = "Email hoặc mật khẩu không đúng!";
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Lỗi đăng nhập: " + ex.Message);
                TempData["ErrorMessage"] = "Có lỗi xảy ra, vui lòng thử lại!";
            }
            return RedirectToAction("DangNhap", "DangNhapDK");
        }

        public ActionResult DangXuat()
        {
            HttpContext.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UserList()
        {
            try
            {
                ViewBag.list = db.get("SELECT * FROM NGUOIDUNG");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Lỗi lấy danh sách người dùng: " + ex.Message);
                TempData["ErrorMessage"] = "Không thể tải danh sách người dùng!";
            }
            return View();
        }
        // Đăng nhập bằng Google
        public void LoginWithGoogle()
        {
            var authenticationProperties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleCallback", "DangNhapDK", null, Request.Url.Scheme) };
            HttpContext.GetOwinContext().Authentication.Challenge(authenticationProperties, "Google");
        }

        //callback của gg
        public ActionResult GoogleCallback()
        {
            var loginInfo = HttpContext.GetOwinContext().Authentication.AuthenticateAsync("ExternalCookie").Result;
            if (loginInfo == null)
                return RedirectToAction("DangNhap");

            // Lấy thông tin người dùng từ Google
            var identity = loginInfo.Identity;
            var googleId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value; // ID Google
            var email = identity.FindFirst(ClaimTypes.Email)?.Value;
            var name = identity.FindFirst(ClaimTypes.Name)?.Value; 
            string matkhau = "GoogleLogin";
            string sdt = "0000000000"; // Số mặc định
            string diachi = "NULL";

            // Kiểm tra email đã tồn tại chưa
            ViewBag.list = db.get($"EXEC KiemTraEmail '{email}',N'{name}'");

            if (ViewBag.list.Count > 0) // 🔹 Nếu email đã tồn tại
            {
                Session["taikhoan"] = ViewBag.list[0][0]; 
                Session["tennguoidung"] = ViewBag.list[0][1]; 
            }
            else // Nếu email chưa có → Thêm tài khoản mới
            {
                db.get($"EXEC ThemNguoiDung N'{name}', '{email}', '{matkhau}', '{sdt}', '{diachi}';");

                // Lấy IDND mới tạo
                ViewBag.list = db.get("EXEC GetNextIDND");
                Session["taikhoan"] = ViewBag.list[0][0];
                Session["tennguoidung"] = name;
            }

            // Đăng nhập vào hệ thống
            var claimsIdentity = new ClaimsIdentity(loginInfo.Identity.Claims, "ApplicationCookie");
            HttpContext.GetOwinContext().Authentication.SignIn(claimsIdentity);

            return RedirectToAction("Index", "Home");
        }

    }
}
