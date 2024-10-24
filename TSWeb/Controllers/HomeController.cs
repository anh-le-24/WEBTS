using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers {
    public class HomeController : Controller {
        DatabaseModel db = new DatabaseModel();
        public ActionResult Index() {
            ViewBag.list = db.get("SELECT * FROM SANPHAM");
            return View();
        }
        public ActionResult Cart() {
            // Lấy IDKH từ session
            if (Session["IDKH"] != null) {
                int idkh = Convert.ToInt32(Session["IDKH"]);
                // Lấy giỏ hàng từ DB dựa trên IDKH
                var cartItems = db.get("EXEC GetCartItems @IDKH", new SqlParameter[] { new SqlParameter("@IDKH", idkh) });

                // Kiểm tra xem giỏ hàng có sản phẩm hay không
                if (cartItems != null && cartItems.Count > 0) {
                    ViewBag.cartItems = cartItems;
                }
                else {
                    ViewBag.cartItems = null; // Nếu không có sản phẩm, gán null
                }
            }
            else {
                ViewBag.cartItems = null; // Nếu không có IDKH, gán null cho cartItems
            }

            return View();
        }


        public ActionResult Chitiet(string idsp) {
            ViewBag.list = db.get("EXEC HienThiSanPhamTheoID " + idsp + ";");
            return View();
        }

        public ActionResult vd() {
            DatabaseModel db = new DatabaseModel();
            ViewBag.list = db.get("select * from SANPHAM");
            return View();
        }
        public ActionResult ThanhToan() {
            return View();
        }
        [HttpPost]
        public ActionResult Themsp(string idsp, string soluong, string size, string idcttp, string idctgh) {
            // Kiểm tra xem IDKH có trong session không
            if (Session["IDKH"] != null) {
                string idkh = Session["IDKH"].ToString();

                // Thực thi thủ tục thêm sản phẩm vào giỏ hàng
                string query = "EXEC AddProductToCart9 @IDKH, @IDSP, @SoLuong, @Size, @IDCTTP, @IDCTGH";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@IDKH", idkh),
            new SqlParameter("@IDSP", idsp),
            new SqlParameter("@SoLuong", soluong),
            new SqlParameter("@Size", size),
            new SqlParameter("@IDCTTP", idcttp),
            new SqlParameter("@IDCTGH", idctgh)
                };

                // Gọi thủ tục và lưu sản phẩm vào session hoặc ViewBag
                db.get(query, parameters);
                // Cập nhật giỏ hàng
                UpdateCart(idkh); // Phương thức để lấy giỏ hàng từ DB và lưu vào ViewBag
                return RedirectToAction("Cart"); // Chuyển hướng đến trang giỏ hàng
            }

            return RedirectToAction("Index"); // Nếu không có IDKH, chuyển hướng về trang chính
        }

        private void UpdateCart(string idkh) {
            // Lấy giỏ hàng từ cơ sở dữ liệu
            string query = "EXEC GetCartItems @IDKH";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@IDKH", idkh)
            };

            var cartItems = db.get(query, parameters); // Giả sử db.get trả về danh sách sản phẩm
            ViewBag.cartItems = cartItems;
        }
    }
}