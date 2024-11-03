
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
        public ActionResult DangKy(string hoten,
                                    string email, 
                                    string matkhau, 
                                    string sdt, 
                                    string diachi) {
            ViewBag.list = db.get("EXEC ThemNguoiDung N'" + hoten + "','" + email + "','" + matkhau + "'," + sdt + ",'" + diachi + "';");
            return RedirectToAction("DangNhap", "DangNhapDk");
        }
        [HttpPost]
        public ActionResult XuLuDangNhap(string email, string matkhau) {
            ViewBag.list = db.get("EXEC DangNhapNG '" + email + "','" + matkhau + "';");
            if (ViewBag.list.Count > 0) {
                Session["taikhoan"] = ViewBag.list[0];
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult DangXuat() {
            Session["taikhoan"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UserList() {
            ViewBag.list = db.get("SELECT * FROM NGUOIDUNG");
            return View();

        }

    }
}