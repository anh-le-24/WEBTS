using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSWeb.Models;
using System.Configuration;
using System.Collections;
using System.CodeDom.Compiler;

namespace TSWeb.Controllers {
    public class HomeController : Controller {
        DatabaseModel db = new DatabaseModel();

        public ActionResult Index() {
            ViewBag.list = db.get("SELECT * FROM SANPHAM");
            return View();
        }
        public ActionResult Cart(int idkh = 1) {
            // Call the stored procedure GetCartDetails2 and pass the customer ID
            string query = $"EXEC GetCartDetails2 @IDKH = {idkh};";

            // Fetch the cart details from the database     
            var cartItems = db.get(query);

            // Pass the result to the ViewBag to make it available in the view
            ViewBag.CartItems = cartItems;

            // Render the view to show the cart details
            return View();
        }

        public ActionResult Chitiet(string idsp) {
            ViewBag.list = db.get("EXEC HienThiSanPhamTheoID " + idsp + ";");
            return View();
        }
        public ActionResult vd() {
            ViewBag.list = db.get("select * from SANPHAM");
            return View();
        }
        public ActionResult ThanhToan() {
            return View();
        }

        [HttpPost]
        public ActionResult Themsp(int idkh, int idsp, int soLuong, string size, int idcttp, int idctgh) {
            try {
                string query = "EXEC AddProductToCart9 @IDKH, @IDSP, @SoLuong, @Size, @IDCTTP, @IDCTGH";

                var parameters = new {
                    IDKH = idkh,
                    IDSP = idsp,
                    SoLuong = soLuong,
                    Size = size,
                    IDCTTP = idcttp,
                    IDCTGH = idctgh
                };

                db.execute(query, parameters);
                return RedirectToAction("Cart", new { idkh = idkh }); // Redirect to Cart action
            }
            catch (Exception ex) {
                ModelState.AddModelError("", $"Có lỗi xảy ra: {ex.Message}");
                return RedirectToAction("Index"); // Redirect to a valid action (Index)
            }
        }
    }
}