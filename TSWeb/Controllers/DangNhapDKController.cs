    using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using TSWeb.Models;


namespace TSWeb.Controllers
{
    public class DangNhapDKController : Controller
    {
         DatabaseModel db =new DatabaseModel();

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(string hoten, string email, string matkhau, string sdt, string diachi)
        {
            ViewBag.list = db.get("EXEC ThemNguoiDung N'" + hoten + "','" + email + "','" + matkhau + "'," +sdt + ",'" + diachi + "';");

            return RedirectToAction("DangKyTC","DangNhapDK");
        }
        //public ActionResult DangKy(FormCollection collection, NGUOIDUNG nd)
        //{
        //    var hoten = collection["Hoten"];
        //    var email = collection["Email"];
        //    var matkhau = collection["Matkhau"];
        //    var nhaplaimatkhau = collection["Nhaplaimk"];
        //    var sdt = collection["Sdt"];
        //    var diachi = collection["Diachi"];
        //    if (String.IsNullOrEmpty(hoten)) {
        //        ViewData["Error1"] = "Họ và tên khách hàng không được trống";
        //    }
        //    if (String.IsNullOrEmpty(email))
        //    {
        //        ViewData["Error2"] = "Email không được bỏ trống";
        //    }
        //    if (String.IsNullOrEmpty(matkhau))
        //    {
        //        ViewData["Error3"] = "Phải nhập mật khẩu";
        //    }
        //    if (String.IsNullOrEmpty(nhaplaimatkhau))
        //    {
        //        ViewData["Error4"] = "Phải nhập lại mật khẩu";
        //    }
        //    if (String.IsNullOrEmpty(sdt))
        //    {
        //        ViewData["Error5"] = "Phải nhập số điện thoại";
        //    }
        //    if (String.IsNullOrEmpty(diachi))
        //    {
        //       ViewData["Error6"] = "Phải nhập địa chỉ";
        //    }
        //    else
        //    {
        //        nd.Hoten = hoten;
        //        nd.Email = email;
        //        nd.Matkhau = matkhau;
        //        nd.Sdt = sdt;
        //        nd.Diachi = diachi;

        //        return RedirectToAction("Index");

        //    }
        //    ViewBag.list = db.get("INSERT INTO NGUOIDUNG (IDND, TenND, Email, MatKhau, Sdt, Diachi, NgayTaoTK, IDVT)");
        //    return View();
        //}
        public ActionResult DangKyTC()
        {
            return View(); 
        }
        
        public ActionResult UserList()
        {
            //DatabaseModel db = new DatabaseModel() ;
            ViewBag.list = db.get("SELECT * FROM NGUOIDUNG");
            return View();
            //if (Session["Email"] == null)
            //{
            //    return RedirectToAction("Login");
            //}

            //DatabaseModel db = new DatabaseModel();
            //string sql = "SELECT UserName, Email FROM NGUOIDUNG";
            //ArrayList userList = db.get(sql);

            //return View(userList);
        }
        
    }
}