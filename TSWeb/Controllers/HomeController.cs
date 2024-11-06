using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers
{
    public class HomeController : Controller
    {
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
        public ActionResult ThemGH(string idnd, string idsp, int SoLuong, int? TongTien, int? tongtientp, string size, string selectedOptions)
        {
            try
            {
                // Kiểm tra giá trị đầu vào
                if (string.IsNullOrEmpty(idnd) || string.IsNullOrEmpty(idsp) || string.IsNullOrEmpty(size))
                {
                    return RedirectToAction("Index", "Home");
                }

                // Xây dựng câu lệnh SQL với các giá trị nhập từ người dùng
                string sqlQuery = "EXEC ThemGioHang " +
                   "@IDKH = " + idnd + ", " +
                   "@SoLuong = " + SoLuong + ", " +
                   "@Gia = " + TongTien + ", " +
                   "@Size = N'" + size + "', " +
                   "@IDSP = " + idsp + ", " +
                   "@DsTopping = N'" + selectedOptions + "', " +
                   "@TongTienTopping = " + tongtientp + ";";

                // Thực thi câu lệnh SQL
                db.get(sqlQuery);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (ghi log hoặc thông báo cho người dùng)
                Console.WriteLine(ex.Message);
                return RedirectToAction("ChiTiet", "Home", new {idsp = idsp});
            }

            return RedirectToAction("ListSanPham", "Home");
        }



        public ActionResult ThanhToan()
        {
            return View();
        }
        public ActionResult ListSanPham()
        {
            ViewBag.list = db.get("SELECT * FROM SANPHAM");
            return View();
        }
    }
}