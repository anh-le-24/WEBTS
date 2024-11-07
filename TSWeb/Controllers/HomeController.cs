using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSWeb.Models;
using System.Collections;
using System.CodeDom.Compiler;

namespace TSWeb.Controllers {
    public class HomeController : Controller {
        DatabaseModel db = new DatabaseModel();

        public ActionResult Index()
        {          
            return View();
        }
        public ActionResult Cart(string id) 
        { 
            ViewBag.list = db.get("EXEC XemTatCaSanPhamGioHang " + id + ";");
            return View();
        }
        public ActionResult Chitiet(string idsp)
    {
            ViewBag.Tp = db.get("select * from TOPPING");
            ViewBag.ctdh = db.get("select * from CTDONHANG");
            ViewBag.list = db.get("EXEC HienThiSanPhamTheoID " + idsp + ";");
            return View();
        }
        [HttpPost]
        public ActionResult ThemGhMoi(
                     int idsp,
                     string size,
                     string selectedOptions,
                     int SoLuong,
                     float? TongTien,
                     float? TongTienTopping)
        {
            try
            {
                // Lấy idnd từ Session và giữ nguyên kiểu string
                string idnd = null;
                if (Session["taikhoan"] != null)
                {
                    idnd = Session["taikhoan"].ToString();
                    ViewBag.Idnd = idnd; // Truyền idnd vào ViewBag
                }
                else
                {
                    // Trường hợp session không tồn tại
                    ViewBag.Idnd = "Không tìm thấy giá trị idnd trong session";
                }


                // Xây dựng câu lệnh SQL với các giá trị nhập từ người dùng
                string sqlCommand = "EXEC ThemGioHang " + Session["taikhoan"].ToString() + ","
                         + SoLuong + ","
                         + (TongTien.HasValue ? TongTien.Value.ToString() : "0") + ","
                         + "'" + size + "',"
                         + idsp + ","
                         + (string.IsNullOrEmpty(selectedOptions) ? "NULL" : "'" + selectedOptions + "'") + ","
                         + (TongTienTopping.HasValue ? TongTienTopping.Value.ToString() : "0") + ";";

                // Thực hiện câu lệnh SQL
                db.get(sqlCommand);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine("Có lỗi xảy ra: " + ex.Message);

                // Redirect lại tới trang chi tiết sản phẩm nếu có lỗi
                return RedirectToAction("ChiTiet", "Home", new { idsp = idsp });
            }

            // Redirect tới trang danh sách sản phẩm
            return RedirectToAction("ListSanPham", "Home");
        }


        public ActionResult ThanhToan()
        {
            return View();
        }
        public ActionResult ListSanPham()
        {
            // Truyền danh sách sản phẩm vào ViewBag
            ViewBag.list = db.get("SELECT * FROM SANPHAM");
            return View();
        }


    }
}