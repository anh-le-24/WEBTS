<<<<<<< HEAD
﻿    using System;
=======
﻿using System;
>>>>>>> vanh
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using TSWeb.Models;
<<<<<<< HEAD

=======
>>>>>>> vanh

namespace TSWeb.Controllers
{
    public class DangNhapDKController : Controller
    {
<<<<<<< HEAD
         DatabaseModel db =new DatabaseModel();

        [HttpGet]
=======
        [HttpGet]
        // GET: DangNhapDK
>>>>>>> vanh
        public ActionResult DangNhap()
        {
            return View();
        }// GET: DangNhapDK
       
        [HttpPost]
        public ActionResult Register(DangNhapDKModels model)
        {
            if (ModelState.IsValid)
            {
                // Check if the username already exists
                DatabaseModel db = new DatabaseModel();
                string sqlCheck = $"SELECT * FROM NGUOIDUNG WHERE Email = '{model.Email}'";
                ArrayList existingUser = db.get(sqlCheck);

                if (existingUser.Count > 0)
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
                    return View(model);
                }

                // Add new user to the database
                string sqlInsert = $"INSERT INTO NGUOIDUNG (TenND, Email, MatKhau, Sdt, DiaChi) VALUES ('{model.TenND}', '{model.Email}', '{model.MatKhau}', '{model.Sdt}'),'{model.Diachi}'";
                db.get(sqlInsert);

                // Redirect to login page after successful registration
                return RedirectToAction("Login");
            }

            return View(model);
        }

        [HttpPost]
<<<<<<< HEAD
        public ActionResult DangKy(string hoten, string email, string matkhau, string sdt, string diachi)
        {
            ViewBag.list = db.get("EXEC ThemNguoiDung N'" + hoten + "','" + email + "','" + matkhau + "'," +sdt + ",'" + diachi + "';");

            return RedirectToAction("DangKyTC","DangNhapDK");
        }
        [HttpPost]
        public ActionResult XuLuDangNhap(string email, string matkhau)
        {
            ViewBag.list = db.get("EXEC KiemTraDangNhap '" + email + "','" + matkhau+"';");
            if (ViewBag.list.Count > 0)
            {
                Session["taikhoan"] = ViewBag.list[0];
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("DangKyTC", "DangNhapDK");
        }
        public ActionResult DangKyTC()
        {
            return View(); 
        }
        
        public ActionResult UserList()
        {
            ViewBag.list = db.get("SELECT * FROM NGUOIDUNG");
            return View();
            
        }
        
=======
        public ActionResult Login(DangNhapDKModels model)
        {
            if (ModelState.IsValid)
            {
                DatabaseModel db = new DatabaseModel();
                string sql = $"SELECT * FROM NGUOIDUNG WHERE Email = '{model.Email}' AND Password = '{model.MatKhau}'";
                ArrayList userData = db.get(sql);

                if (userData.Count > 0)
                {
                    Session["Email"] = model.Email;
                    return RedirectToAction("UserList");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }

            return View(model);
        }
        // Logout action
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult UserList()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }

            DatabaseModel db = new DatabaseModel();
            string sql = "SELECT UserName, Email FROM NGUOIDUNG";
            ArrayList userList = db.get(sql);

            return View(userList);
        }

>>>>>>> vanh
    }
}