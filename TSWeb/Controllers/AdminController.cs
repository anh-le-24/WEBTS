using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DatabaseModel db = new DatabaseModel();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QLKhachHang()
        {
            ViewBag.list = db.get("Exec XemNguoiDung 3");
            return View();
        }
        public ActionResult ThemKhachHang()
        {

            return View(); 
        }

        [HttpPost]
        public ActionResult ThemKH(string FullName,
                                   string Email,
                                   string Password,
                                   int PhoneNumber,
                                   string Address,
                                   DateTime DateCreate)
        {
            try
            {
                // Chuyển DateCreate sang định dạng phù hợp với SQL Server
                string formattedDate = DateCreate.ToString("yyyy-MM-dd HH:mm:ss");

                // Xây dựng câu lệnh SQL với phép nối chuỗi như cũ nhưng có format lại cho chính xác
                string sqlQuery = "EXEC ThemKhachHangVaNguoiDung N'" + FullName + "', N'" + Email + "', N'" + Password + "', "
                                  + PhoneNumber + ", N'" + Address + "', '" + formattedDate + "', 3, 0, 1;";

                // Thực hiện câu lệnh SQL
                db.get(sqlQuery);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu có
                Console.WriteLine(ex.Message);
                return View("ThemKhachHang");
            }

            // Nếu thành công, chuyển hướng đến trang quản lý khách hàng
            return RedirectToAction("QLKhachHang", "Admin");
        }

        public ActionResult ChinhSuaKhachHang(string id)
        {
            ViewBag.list = db.get("Exec XemNguoiDungTheoID " + id + ";");
            return View();
        }
        [HttpPost]
        public ActionResult ChinhSuaKH(int IDND,
                                       string FullName,
                                       string Email,
                                       string Password,
                                       int PhoneNumber,
                                       string Address)
        {
            try
            {
                // Chuyển DateCreate sang định dạng phù hợp với SQL Server
                string formattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Xây dựng câu lệnh SQL với phép nối chuỗi như cũ nhưng có format lại cho chính xác
                string sqlQuery = "EXEC SuaNguoiDungKhachHang " + IDND + ", N'" + FullName + "', N'" + Email + "', N'" + Password + "', "
                                  + PhoneNumber + ", N'" + Address + "', '" + formattedDate + "', 3, 0, 0 ;";

                // Thực hiện câu lệnh SQL
                db.get(sqlQuery);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu có
                Console.WriteLine(ex.Message);
                return View("ChinhSuaKhachHang");
            }

            // Nếu thành công, chuyển hướng đến trang quản lý khách hàng
            return RedirectToAction("QLKhachHang", "Admin");
        }
        public ActionResult XoaKhachHang(string id)
        {
            ViewBag.list = db.get("exec XoaNguoiDungKhachHang " + id);
            return RedirectToAction("QLKhachHang","Admin"); 
        }
        // Các phương thức quản lý nhân viên
        public ActionResult QLNhanVien()
        {
            ViewBag.list = db.get("Exec XemNguoiDung 2");
            return View();
        }
        public ActionResult ThemNV()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemNhanVien(string FullName,
                                         string Email,
                                         string Password,
                                         int PhoneNumber,
                                         string Address,
                                         DateTime DateCreate,
                                         string CN)
        {
            try
            {
                // Chuyển DateCreate sang định dạng phù hợp với SQL Server
                string formattedDate = DateCreate.ToString("yyyy-MM-dd HH:mm:ss");

                // Xây dựng câu lệnh SQL với phép nối chuỗi như cũ nhưng có format lại cho chính xác
                string sqlQuery = "EXEC ThemNhanVienVaNguoiDung N'" + FullName + "','" + Email + "', N'" + Password + "', "
                                  + PhoneNumber + ", N'" + Address + "', '" + formattedDate + "', 2, " + CN + ";";

                // Thực hiện câu lệnh SQL
                db.get(sqlQuery);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu có
                Console.WriteLine(ex.Message);
                return View("ThemNV");
            }

            // Nếu thành công, chuyển hướng đến trang quản lý nhân viên
            return RedirectToAction("QLNhanVien", "Admin");
        }
        public ActionResult ChinhSuaNV()
        {

            return View();
        }

        public ActionResult XoaNV(string id)
        {
            ViewBag.list = db.get("EXEC XoaNguoiDungNhanVien " + id + ";");
            return RedirectToAction("QLNhanVien", "Admin");
        }
    }
}