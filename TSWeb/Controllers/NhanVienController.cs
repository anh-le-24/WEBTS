using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers {
    public class NhanVienController : Controller {
        DatabaseModel db = new DatabaseModel();
        public ActionResult DNNV() {
            return View();
        }
        public ActionResult HomeNV() {
            ViewBag.list = db.get("select * from DONHANG");
            return View();
        }
        public ActionResult ThemDH() {
            ViewBag.list = db.get("select * from SANPHAM");
            ViewBag.Tp = db.get("select * from TOPPING");
            ViewBag.ctdh = db.get("select * from CTDONHANG");
            return View();
        }
        public ActionResult CaLam() {
            ViewBag.listCL = db.get("SELECT * FROM CALAM");
            return View();
        }
        [HttpPost]
        public ActionResult DkyCL(string calam, string ngaylam) {
            try {
                // Retrieve IDNV from session
                /*var idnv = ((System.Collections.ArrayList)Session["taikhoan"])[0];*/ // Assuming IDNV is the first element
                Session["taikhoan"] = new System.Collections.ArrayList { 8 };
                // Thực thi Store Procedure DangKyCaLam với các tham số
                db.get("EXEC DangKyCaLam N'" + calam + "', '" + ngaylam + "';");

                // Thông báo đăng ký ca làm thành công
                ViewBag.SuccessMessage = "Đăng ký ca làm thành công!";
            }
            catch (Exception ex) {
                // Xử lý lỗi nếu có
                ViewBag.ErrorMessage = "Có lỗi xảy ra: " + ex.Message;
            }

            return RedirectToAction("CaLam", "NhanVien");
        }
    }
}