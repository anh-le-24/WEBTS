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
using System.Net.NetworkInformation;
using System.Data;

namespace TSWeb.Controllers {
    public class HomeController : Controller {
        DatabaseModel db = new DatabaseModel();

        public ActionResult Index()
        {          
            return View();
        }
        public ActionResult Cart(string id) 
        {
            ViewBag.nd = db.get("Exec  XemNguoiDungTheoID " + id);
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
              int TongTien,   // Thay đổi kiểu từ float? thành int?
              int? TongTienTopping)
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
                         + TongTien + ","  // Kiểm tra và sử dụng giá trị TongTien là int
                         + "'" + size + "',"
                         + idsp + ","
                         + (string.IsNullOrEmpty(selectedOptions) ? "NULL" : "N'" + selectedOptions + "'") + ","
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


        public ActionResult XoaGH(string id)
        {
            ViewBag.list= db.get("EXEC XoaSanPhamTrongGioHang "+ id);
            return RedirectToAction("Cart","Home",new {id = Session["taikhoan"] });
        }

        public ActionResult SuaGH(
            string idcttp,
            int SoLuong,
            int tongtienSP)
        {
            db.get("EXEC SuaSoLuongGiaCTTOPING " + Session["taikhoan"] + "," + idcttp + "," + SoLuong + "," + tongtienSP + ";");
            return RedirectToAction(@Url.Action("ThanhToan", "Home"));
        }

        public ActionResult ThanhToan(string id) {
            ViewBag.nd = db.get("Exec  XemNguoiDungTheoID " + id);
            ViewBag.list = db.get("EXEC XemTatCaSanPhamGioHang " + id + ";");
            ViewBag.listVC = db.get("EXEC XemVoucherDaLuu " + Session["taikhoan"]);
            ViewBag.listD = db.get("EXEC GetDiemTichLuyTheoIDND " + Session["taikhoan"]);
            return View();
        }

        [HttpPost]
        public ActionResult ThanhToanHD(int finalTotal, string phuongthanhtoan, string voucherId, int loyaltyPointsUsed = 0) {
            try {
                DateTime date = DateTime.Today;  // Ngày hiện tại
                string dateTH = phuongthanhtoan == "Thanh toán khi nhận hàng" ? "NULL" : $"'{date.ToString("yyyy-MM-dd")}'";

                // Xóa voucher nếu có
                if (!string.IsNullOrEmpty(voucherId)) {
                    db.get($"EXEC XoaVoucherDaLuu '{voucherId}', {Session["taikhoan"]}");
                }

                // Trừ điểm tích lũy nếu có
                if (loyaltyPointsUsed > 0) {
                    var isSuccess = db.get($"EXEC TruDiemTichLuy {Session["taikhoan"]}, {loyaltyPointsUsed}");
                    if (isSuccess == null) {
                        throw new Exception("Không thể trừ điểm tích lũy.");
                    }
                }

                // Tạo câu lệnh SQL để thêm đơn hàng
                string sqlCommand = $"EXEC ThemDonHang '{date.ToString("yyyy-MM-dd")}', N'Đang xử lý', {finalTotal}, "
                                    + $"{Session["taikhoan"]}, {dateTH}, N'{phuongthanhtoan}'";

                // Thực thi câu lệnh SQL để lưu đơn hàng
                db.get(sqlCommand);

                // Chuyển hướng đến trang TTTC sau khi đặt hàng thành công
                return RedirectToAction("TTTC", "Home");
            }
            catch (Exception ex) {
                // Ghi lại lỗi để kiểm tra
                // Có thể sử dụng logger để ghi lại lỗi hoặc ghi vào Debug
                System.Diagnostics.Debug.WriteLine(ex.Message);

                // Nếu có lỗi, chuyển hướng về trang thanh toán
                return RedirectToAction("ThanhToan", "Home", new { id = Session["taikhoan"] });
            }
        }


        public ActionResult TTTC()
        {
            return View();
        }
        public ActionResult ListSanPham()
        {
            // Truyền danh sách sản phẩm vào ViewBag
            ViewBag.list = db.get("SELECT * FROM SANPHAM");
            return View();
        }

        public ActionResult GioiThieu()
        {
            return View();
        }

        public ActionResult Discount()
        {
            ViewBag.listVC = db.get("SELECT * FROM MAGIAMGIA");
            return View();
        }

        [HttpPost]
        public ActionResult SaveVch(string idvc) {
            ViewBag.list = db.get("EXEC LuuVoucherChoKhachHang " +idvc+ "," + Session["taikhoan"]);
            return RedirectToAction("Discount", "Home");
        }

    }
}