
using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.PortableExecutable;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers {
    public class DangNhapDKController : Controller {

        DatabaseModel db = new DatabaseModel();
        [HttpGet]
        public ActionResult DangNhap() {
            return View();
        }// GET: DangNhapDK

        [HttpPost]
        public ActionResult DangKy(string hoten, string email, string matkhau, string sdt, string diachi)
        {
            ViewBag.list = db.get("EXEC ThemNguoiDung N'" + hoten + "','" + email + "','" + matkhau + "'," + sdt + ",'" + diachi + "';");
            return RedirectToAction("DangNhap", "DangNhapDK");
        }
        [HttpPost]
        public ActionResult XuLuDangNhap(string email, string matkhau)
        {
            // Lấy thông tin từ cơ sở dữ liệu (có thể là một mảng hoặc một danh sách)
            ViewBag.list = db.get("EXEC DangNhapNG '" + email + "','" + matkhau + "';");

            if (ViewBag.list.Count > 0)
            {
                // Lấy thông tin người dùng từ ViewBag.list[0]
                var user = ViewBag.list[0];

                // Kiểm tra kiểu dữ liệu của user và lấy id
                if (user is ArrayList userList && userList.Count > 0)
                {
                    // Giả sử userList[0] là id người dùng
                    string userId = userList[0].ToString(); // Lấy id từ ArrayList
                    int idnd = 0;
                    int.TryParse(userId, out idnd); // Chuyển đổi từ string sang int


                    string userName = userList[2].ToString();
                    // Lưu id người dùng vào session
                    Session["taikhoan"] = idnd;
                    Session["tennguoidung"] = userName;

                    return RedirectToAction("Index", "Home");
                }
            }

            // Nếu không tìm thấy người dùng hợp lệ, chuyển về trang đăng nhập
            return RedirectToAction("DangNhap", "DangNhapDK");
        }


        [HttpPost]
        public ActionResult DangXuat()
        {
            Session["taikhoan"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UserList() {
            ViewBag.list = db.get("SELECT * FROM NGUOIDUNG");
            return View();

        }

    }
}