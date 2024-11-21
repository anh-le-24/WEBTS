using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers
{
    public class NhanVienController : Controller
    {
        DatabaseModel db = new DatabaseModel();

        // View cho Đăng nhập
        public ActionResult DNNV()
        {
            return View();
        }

        // API xác nhận đăng nhập
        [HttpPost]
        public JsonResult XNDNNVApi(string email, string password)
        {
            var result = db.get("EXEC KiemTraDangNhapNV '" + email + "','" + password + "';");
            bool isSuccess = result != null && result.Count > 0; 

            return Json(new { success = isSuccess }, JsonRequestBehavior.AllowGet);
        }

        // Action xác nhận đăng nhập, chuyển hướng đến Home
        [HttpPost]
        public ActionResult XNDNNV(string email, string password)
        {
            // Lấy dữ liệu người dùng từ thủ tục kiểm tra đăng nhập
            ViewBag.list = db.get("EXEC KiemTraDangNhapNV '" + email + "','" + password + "';");

            if (ViewBag.list.Count > 0)
            {
                var user = ViewBag.list[0];

                if (user is ArrayList userList && userList.Count > 0)
                {
                    string userId = userList[0].ToString();
                    int idnd = 0;
                    int.TryParse(userId, out idnd);

                    string userName = userList[1].ToString();

                    // Gọi thủ tục lấy vai trò người dùng từ idnd
                    var roleCheck = db.get("EXEC GetUserRoleById " + idnd);  // Gọi thủ tục GetUserRoleById

                    if (roleCheck.Count > 0)
                    {
                        var roleInfo = roleCheck[0] as ArrayList;
                        if (roleInfo != null && roleInfo.Count > 0)
                        {
                            int idvt = 0;
                            int.TryParse(roleInfo[0].ToString(), out idvt);  // Lấy giá trị idvt

                            // Lưu thông tin người dùng vào session
                            Session["taikhoanNV"] = idnd;
                            Session["tennguoidungNV"] = userName;

                            // Kiểm tra nếu idvt = 1 (Admin)
                            if (idvt == 1)
                            {
                                return RedirectToAction("Index", "Admin");
                            }
                            else
                            {
                                return RedirectToAction("HomeNV", "NhanVien");
                            }
                        }
                        else
                        {
                            // Nếu không có thông tin vai trò
                            // Xử lý lỗi khi không có vai trò
                            ViewBag.ErrorMessage = "Không tìm thấy vai trò cho người dùng.";
                            return View();
                        }
                    }
                    else
                    {
                        // Nếu không gọi được thủ tục hoặc không trả về dữ liệu
                        ViewBag.ErrorMessage = "Lỗi khi lấy vai trò người dùng.";
                        return View();
                    }
                }
            }

            // Nếu không đăng nhập thành công, quay lại trang đăng nhập
            return RedirectToAction("DNNV", "NhanVien");
        }



        [HttpPost]
        public ActionResult TaoDH(int? finalTotal, string phuongthanhtoan)
        {
            try
            {
                // Kiểm tra nếu finalTotal không có giá trị
                if (!finalTotal.HasValue)
                {
                    // Bạn có thể xử lý trường hợp này theo yêu cầu của bạn, ví dụ:
                    finalTotal = 0; // Hoặc gán một giá trị mặc định khác
                }

                DateTime date = DateTime.Today;
                string dateTH = phuongthanhtoan == "Thanh toán khi nhận hàng" ? "NULL" : $"'{date:yyyy-MM-dd}'";

                // Tạo chuỗi lệnh SQL
                string sqlCommand = $"EXEC ThemDonHang '{date:yyyy-MM-dd}', N'Đã xác nhận', {finalTotal.Value}, "
                                    + $"{Session["taikhoanNV"]}, {dateTH}, N'{phuongthanhtoan}'";

                // Thực hiện lưu vào cơ sở dữ liệu
                db.get(sqlCommand);
            }
            catch (Exception)
            {
                // Nếu có lỗi, chuyển hướng về trang đăng nhập nhân viên
                return RedirectToAction("DNNV", "NhanVien");
            }

            // Sau khi hoàn tất, chuyển hướng đến trang thêm đơn hàng
            return RedirectToAction("ThemDH", "NhanVien");
        }


        // View cho Home
        public ActionResult HomeNV()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangKH");
            return View();
        }

        // API cho Home
        [HttpGet]
        public JsonResult HomeNVApi()
        {
            var orders = db.get("EXEC XemTatCaDonHangKH");
            return Json(new { data = orders }, JsonRequestBehavior.AllowGet);
        }

        // View cho thêm đơn hàng
        public ActionResult ThemDH()
        {
            ViewBag.gh = db.get("EXEC XemTatCaSanPhamGioHang 16;");
            ViewBag.list = db.get("select * from SANPHAM");
            ViewBag.tp = db.get("select * from TOPPING");
            return View();
        }
        [HttpPost]
        public ActionResult ThemSPDH(int idsp,
              string size,
              string selectedOptions,
              int SoLuong,
              int TongTien,   // Kiểu int cho giá trị tổng tiền
              int? TongTienTopping)
        {
            try
            {
                // Xây dựng câu lệnh SQL với các giá trị nhập từ người dùng
                string sqlCommand = "EXEC ThemGioHang 16,"
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

                return RedirectToAction("DNNV", "NhanVien");
            }

            return RedirectToAction("ThemDH", "NhanVien");
        }


        // Các view trạng thái đơn hàng
        public ActionResult DonchoXN()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoTrangThai N'Đang Xử lý';");
            return View();
        }

        public ActionResult DonDaXN()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoTrangThai N'Đã xác nhận';");
            return View();
        }

        public ActionResult DonDangGiao()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoTrangThai N'Đang giao';");
            return View();
        }

        public ActionResult DonDaHT()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoTrangThai N'Đã hoàn thành';");
            return View();
        }

        public ActionResult DonBiHuy()
        {
            ViewBag.list = db.get("EXEC XemTatCaDonHangTheoTrangThai N'Bị hủy';");
            return View();
        }

        // API cập nhật trạng thái đơn hàng
        [HttpPost]
        public JsonResult XNApi(string iddh, string trangthai)
        {
            db.get("EXEC CapNhatTrangThaiDonHang " + iddh + ",N'" + trangthai + "';");
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // Action cập nhật trạng thái đơn hàng
        [HttpPost]
        public ActionResult XN(string iddh, string trangthai)
        {
            db.get("EXEC CapNhatTrangThaiDonHang " + iddh + ",N'" + trangthai + "';");
            return RedirectToAction("HomeNV");
        }

        // View cho báo cáo lỗi
        public ActionResult BaoCaoLoi()
        {
            return View();
        }

        // API gửi báo cáo lỗi
        [HttpPost]
        public JsonResult GuiBCApi(string tieude, string noidung)
        {
            db.get(" EXEC AddNotificationForRole N'" + tieude + "', N'" + noidung + "';");
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // Action gửi báo cáo lỗi
        [HttpPost]
        public ActionResult GuiBC(string tieude, string noidung)
        {
            db.get(" EXEC AddNotificationForRole N'" + tieude + "', N'" + noidung + "';");
            return RedirectToAction("BaoCaoLoi", "NhanVien");
        }

        public ActionResult Xoa(string id)
        {
            ViewBag.list = db.get("EXEC XoaSanPhamTrongGioHang " + id);
            return RedirectToAction("ThemDH", "NhanVien");
        }
    }
}
