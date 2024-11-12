using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSWeb.Models;

namespace TSWeb.Controllers
{
    public class AdminController : Controller
    {
        DatabaseModel db = new DatabaseModel();

        public ActionResult Index()
        {
            return View();  // Trang Index của sản phẩm
        }

        public ActionResult ĐonHang()
        {

            return View();
        }
        public ActionResult ThongBao()
        {

            return View();
        }
        public ActionResult ChiNhanh()
        {

            return View();
        }
        public ActionResult TongQuan()
            {
                return View();
            }
       /* // API endpoint để lấy dữ liệu thống kê theo tháng
        [HttpPost]
        public JsonResult GetStatistics(int year)
        {
            List<MonthlyRevenue> monthlyRevenueList = new List<MonthlyRevenue>();

            try
            {
                // Lấy dữ liệu thống kê doanh thu theo tháng
                string sqlQueryRevenue = "EXEC ThongKeDoanhThuTheoThang @Nam";
                SqlParameter[] revenueParameters = new SqlParameter[]
                {
                new SqlParameter("@Nam", year)
                };
                var revenueResult = db.get(sqlQueryRevenue, revenueParameters);

                // Duyệt qua kết quả doanh thu hàng tháng
                foreach (ArrayList row in revenueResult)
                {
                    if (row != null && row.Count > 1) // Đảm bảo rằng hàng có ít nhất 2 cột
                    {
                        int thang = Convert.ToInt32(row[0]); // Chuyển kiểu đúng
                        decimal doanhThu = Convert.ToDecimal(row[1]); // Chuyển kiểu đúng

                        monthlyRevenueList.Add(new MonthlyRevenue
                        {
                            Thang = thang,
                            DoanhThu = doanhThu
                        });
                    }
                }

                // Lấy dữ liệu thống kê số lượng khách hàng
                string sqlQueryCustomers = "SELECT COUNT(*) AS TongSoKhachHang FROM KHACHHANG";
                var customerResult = db.get(sqlQueryCustomers);
                int totalCustomers = 0;
                if (customerResult != null && customerResult.Count > 0 && customerResult[0] is ArrayList customerRow && customerRow.Count > 0)
                {
                    totalCustomers = Convert.ToInt32(customerRow[0]); // Chuyển kiểu đúng
                }

                // Lấy dữ liệu thống kê số lượng đơn hàng
                string sqlQueryOrders = "SELECT COUNT(*) AS TongSoDonHang FROM THANHTOAN";
                var orderResult = db.get(sqlQueryOrders);
                int totalOrders = 0;
                if (orderResult != null && orderResult.Count > 0 && orderResult[0] is ArrayList orderRow && orderRow.Count > 0)
                {
                    totalOrders = Convert.ToInt32(orderRow[0]); // Chuyển kiểu đúng
                }

                // Trả về dữ liệu thống kê dưới dạng JSON
                return Json(new
                {
                    monthlyRevenue = monthlyRevenueList,
                    totalCustomers = totalCustomers,
                    totalOrders = totalOrders
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log exception (ví dụ, dùng Log4Net hoặc một công cụ log khác)
                return Json(new { success = false, message = "Đã xảy ra lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // Lớp MonthlyRevenue để chứa thông tin doanh thu theo tháng
        public class MonthlyRevenue
        {
            public int Thang { get; set; }
            public decimal DoanhThu { get; set; }
        }


        public ActionResult ChinhSuaCN(string id)
        {
            ViewBag.list = db.get("Exec XemChiNhanh @IDCN= " + id + ";");
            return View();
        }
        public ActionResult QLChiNhanh()
        {
            ViewBag.list = db.get("Exec XemTatCaChiNhanh");
            return View();
        }
        public ActionResult ThemCN()
        {
            return View();
        }

        // Các phương thức quản lý thanh toán
        public ActionResult QLThanhToan()
        {
            ViewBag.list = db.get("EXEC XemTatCaThanhToan");
            return View();
        }
        // Xem chi tiết thanh toán

        public ActionResult XemChiTietThanhToan(string id)
        {
            ViewBag.list = db.get("EXEC XemChiTietThanhToan " + id);
            return View();
        }
        //Thêm thanh toán mới
        public ActionResult ThemThanhToan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemTT(DateTime NgayThanhToan, double TongTien, string HinhThucThanhToan, int IDDH)
        {
            try
            {
                string sqlQuery = "EXEC ThemThanhToan '" + NgayThanhToan.ToString("yyyy-MM-dd") + "', " + TongTien + ", N'" + HinhThucThanhToan + "', " + IDDH + ";";
                db.get(sqlQuery);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("ThemThanhToan");
            }
            return RedirectToAction("QLThanhToan", "Admin");
        }
        //Chỉnh sửa thanh toán
        public ActionResult ChinhSuaThanhToan(string id)
        {
            ViewBag.list = db.get("EXEC XemChiTietThanhToan " + id);
            return View();
        }

        [HttpPost]
        public ActionResult ChinhSuaTT(int IDTT, DateTime NgayThanhToan, double TongTien, string HinhThucThanhToan, int IDDH)
        {
            try
            {
                string sqlQuery = "EXEC CapNhatThanhToan " + IDTT + ", '" + NgayThanhToan.ToString("yyyy-MM-dd") + "', " + TongTien + ", N'" + HinhThucThanhToan + "', " + IDDH + ";";
                db.get(sqlQuery);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("ChinhSuaThanhToan");
            }
            return RedirectToAction("QLThanhToan", "Admin");
        }

        // Xóa thanh toán
        public ActionResult XoaThanhToan(string id)
        {
            db.get("EXEC XoaThanhToan " + id);
            return RedirectToAction("QLThanhToan", "Admin");
        }



        // Các phương thức quản lý khách hàng

        public ActionResult QLKhachHang()
        {
            ViewBag.list = db.get("Exec XemNguoiDung 3");
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
                string formattedDate = DateCreate.ToString("dd-MM-yyyy HH:mm:ss");

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

        public ActionResult ThemKhachHang()
        {
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
                string formattedDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

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

        public ActionResult ChinhSuaKhachHang(string id)
        {
            ViewBag.list = db.get("Exec XemNguoiDungTheoID " + id + ";");
            return View();
        }

        public ActionResult XoaKhachHang(string id)
        {
            ViewBag.list = db.get("exec XoaNguoiDungKhachHang " + id);
            return RedirectToAction("QLKhachHang", "Admin");
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

        public ActionResult ChinhSuaNV(string id)
        {
            ViewBag.list = db.get("Exec XemNguoiDungTheoID " + id + ";");
            return View();
        }
        [HttpPost]
        public ActionResult ChinhSuaNhanVien(string FullName,
                                         string Email,
                                         string Password,
                                         int PhoneNumber,
                                         string Address,
                                         string CN)
        {
            try
            {
                // Chuyển DateCreate sang định dạng phù hợp với SQL Server
                //string formattedDate = DateCreate.ToString("yyyy-MM-dd HH:mm:ss");

                // Xây dựng câu lệnh SQL với phép nối chuỗi như cũ nhưng có format lại cho chính xác
                string sqlQuery = "EXEC ThemNhanVienVaNguoiDung N'" + FullName + "','" + Email + "', N'" + Password + "', "
                                  + PhoneNumber + ", N'" + Address + *//*"', '" + formattedDate +*//* "', 2, " + CN + ";";

                // Thực hiện câu lệnh SQL
                db.get(sqlQuery);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu có
                Console.WriteLine(ex.Message);
                return View("ChinhSuaNhanVien");
            }

            // Nếu thành công, chuyển hướng đến trang quản lý nhân viên
            return RedirectToAction("QLNhanVien", "Admin");
        }
        public ActionResult XoaNV(string id)
        {
            ViewBag.list = db.get("EXEC XoaNguoiDungNhanVien " + id);
            return RedirectToAction("QLNhanVien", "Admin");
        }

        //--
        public ActionResult QLUuDai(string tenUuDai = "", DateTime? ngayThem = null)
        {
            DatabaseModel db = new DatabaseModel();
            string query = "SELECT * FROM MAGIAMGIA WHERE 1=1";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(tenUuDai))
            {
                query += " AND TenMa LIKE '%' + @tenUuDai + '%'";
                parameters.Add(new SqlParameter("@tenUuDai", tenUuDai));
            }

            if (ngayThem.HasValue)
            {
                query += " AND CONVERT(DATE, NgayBD) = @ngayThem";
                parameters.Add(new SqlParameter("@ngayThem", ngayThem.Value));
            }

            var list = db.get(query, parameters.ToArray());
            ViewBag.list = list;
            return View();
        }


        public ActionResult ThemUuDai()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemUuDaiMoi(int IDVC, string TenMa, string MoTa, DateTime NgayBD, DateTime NgayKT, float PhanTramGiam, int DieuKien)
        {
            if (!ModelState.IsValid)
            {
                return View("ThemUuDai");
            }

            try
            {
                string sql = $"EXEC AddMagiamgia {IDVC}, N'{TenMa}', N'{MoTa}', '{NgayBD:yyyy-MM-dd}', '{NgayKT:yyyy-MM-dd}', {PhanTramGiam}, {DieuKien}";
                db.get(sql);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi thêm ưu đãi: " + ex.Message);
                return View("ThemUuDai");
            }

            return RedirectToAction("QLUuDai", "Admin");
        }


        public ActionResult ChinhSuaUuDai(int id)
        {
            // Lấy thông tin ưu đãi từ cơ sở dữ liệu
            var offerData = db.get("SELECT * FROM MAGIAMGIA WHERE IDVC = " + id);

            // Kiểm tra dữ liệu trả về
            if (offerData == null || offerData.Count == 0)
            {
                return HttpNotFound(); // Trả về lỗi 404 nếu không tìm thấy
            }

            // Chuyển dữ liệu vào ViewBag
            ViewBag.uuDai = offerData;

            return View();
        }

        [HttpPost]
        public ActionResult CapNhatUuDai(string IDVC, string TenMa, string MoTa, string NgayBD, string NgayKT, float PhanTramGiam, int DieuKien)
        {
            if (!ModelState.IsValid)
            {
                return View("ChinhSuaUuDai");
            }

            try
            {
                DateTime ngayBatDau, ngayKetThuc;

                // Kiểm tra format ngày
                if (!DateTime.TryParse(NgayBD, out ngayBatDau) || !DateTime.TryParse(NgayKT, out ngayKetThuc))
                {
                    ModelState.AddModelError("", "Định dạng ngày không hợp lệ");
                    var offerData = db.get("SELECT * FROM MAGIAMGIA WHERE IDVC = " + IDVC);
                    ViewBag.uuDai = offerData;
                    return View("ChinhSuaUuDai");
                }

                // Kiểm tra ngày bắt đầu không được sau ngày kết thúc
                if (ngayBatDau > ngayKetThuc)
                {
                    ModelState.AddModelError("", "Ngày bắt đầu không thể sau ngày kết thúc");
                    var offerData = db.get("SELECT * FROM MAGIAMGIA WHERE IDVC = " + IDVC);
                    ViewBag.uuDai = offerData;
                    return View("ChinhSuaUuDai");
                }

                string sql = "EXEC CapNhatMaGiamGia " +
                    "@IDVC = " + IDVC + ", " +
                    "@TenMa = N'" + TenMa + "', " +
                    "@MoTa = N'" + MoTa + "', " +
                    "@NgayBD = '" + ngayBatDau.ToString("yyyy-MM-dd") + "', " +
                    "@NgayKT = '" + ngayKetThuc.ToString("yyyy-MM-dd") + "', " +
                    "@PhanTramGiam = " + PhanTramGiam + ", " +
                    "@DieuKien = " + DieuKien;

                db.get(sql);
                return RedirectToAction("QLUuDai", "Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật ưu đãi: " + ex.Message);
                var offerData = db.get("SELECT * FROM MAGIAMGIA WHERE IDVC = " + IDVC);
                ViewBag.uuDai = offerData;
                return View("ChinhSuaUuDai");
            }
        }



        [HttpPost]
        public ActionResult XoaMaGiamGia(string id)
        {
            ViewBag.id = db.get("EXEC XoaMaGiamGia " + id + ";");
            return RedirectToAction("QLUuDai", "Admin");
        }
        public ActionResult QLSanPham(string tenSanPham, string ngayThem)
{
    DatabaseModel db = new DatabaseModel();

    // Tạo truy vấn SQL với điều kiện tìm kiếm và lọc
    string query = "SELECT * FROM SANPHAM WHERE 1=1";
    
    if (!string.IsNullOrEmpty(tenSanPham))
    {
        query += $" AND TenSP LIKE N'%{tenSanPham}%'";
    }

    if (!string.IsNullOrEmpty(ngayThem))
    {
        query += $" AND CONVERT(DATE, NgayTao) = '{ngayThem}'";
    }

    var list = db.get(query); // Lấy sản phẩm từ cơ sở dữ liệu theo điều kiện
    ViewBag.list = list;
    return View();
}

        public ActionResult ThemSanPham()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemSPMoi(string TenSP,
                                   HttpPostedFileBase HinhAnh,
                                   float Gia,
                                   int GiamGia,
                                   DateTime NgayTao,
                                   string MoTa,
                                   int IDVC,
                                   int IDDM)
        {
            // Kiểm tra và lưu hình ảnh
            string fileName = null;
            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                fileName = Path.GetFileName(HinhAnh.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Picture"), fileName);
                HinhAnh.SaveAs(path);
            }

            // Gọi stored procedure
            try
            {
                // Xây dựng câu lệnh SQL
                string sql = "EXEC ThemSanPham N'" + TenSP + "', " + Gia + ", N'" +
                             (fileName ?? "NULL") + "', '" +
                             NgayTao.ToString("yyyy-MM-dd") + "', N'" +
                             MoTa + "', " + GiamGia + ", " + IDVC + ", " + IDDM + ";";

                db.get(sql);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi, có thể ghi log hoặc trả thông báo lỗi
                ModelState.AddModelError("", "Có lỗi xảy ra trong quá trình thêm sản phẩm: " + ex.Message);
                return View("ThemSanPham"); // Trả lại view để người dùng sửa lỗi
            }

            return RedirectToAction("QLSanPham", "Admin");
        }


        [HttpPost]
        public ActionResult ChinhSuaSanPham(string IDSP,
                                                   string TenSP,
                                                   HttpPostedFileBase HinhAnh,
                                                   float Gia,
                                                   int GiamGia,
                                                   DateTime NgayTao,
                                                   string MoTa,
                                                   int IDVC,
                                                   int IDDM)
        {
            string fileName = null;

            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                fileName = Path.GetFileName(HinhAnh.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Picture"), fileName);
                HinhAnh.SaveAs(path);
            }

            // Khởi tạo đối tượng DatabaseModel
            try
            {
                string sql = "EXEC  SuaSanPham " + IDSP + ", N'" + TenSP + "', " + Gia + ", " +
                      (string.IsNullOrEmpty(fileName) ? "NULL" : "N'" + fileName + "'") + ", '" +
                      NgayTao.ToString("yyyy-MM-dd") + "', N'" +
                      MoTa + "', " + GiamGia + ", " + IDVC + ", " + IDDM;

                // Gọi phương thức thực thi câu lệnh SQL
                db.get(sql);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra trong quá trình cập nhật sản phẩm: " + ex.Message);
                return View("ChinhSuaSanPham"); // Trả lại view để người dùng sửa lỗi
            }
            return RedirectToAction("QLSanPham", "Admin");
        }

        public ActionResult ChinhSuaSP(string id)
        {

            ViewBag.list = db.get("SELECT * FROM SANPHAM WHERE IDSP = " + id);
            ViewBag.danhmuc = db.get("SELECT * FROM DANHMUC ");
            ViewBag.voucher = db.get("SELECT * FROM MAGIAMGIA ");
            return View();
        }


        [HttpPost]
        public ActionResult XoaSanPham(string id)
        {
            ViewBag.id = db.get("EXEC XoaSanPham " + id + ";");
            return RedirectToAction("QLSanPham", "Admin");
        }




        public ActionResult Topping(string tenTopping = "", DateTime? ngayTao = null)
        {
            DatabaseModel db = new DatabaseModel();
            string query = "SELECT * FROM TOPPING WHERE 1=1";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(tenTopping))
            {
                query += " AND TenTP LIKE '%' + @tenTopping + '%'";
                parameters.Add(new SqlParameter("@tenTopping", tenTopping));
            }

            if (ngayTao.HasValue)
            {
                query += " AND CONVERT(DATE, NgayTao) = @ngayTao";
                parameters.Add(new SqlParameter("@ngayTao", ngayTao.Value));
            }

            var list = db.get(query, parameters.ToArray());
            ViewBag.list = list;
            return View();
        }


        public ActionResult ThemTopping()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemToppingMoi(int? IDTP, string TenTP, float GiaTP, int? IDSP)
        {
            if (!ModelState.IsValid)
            {
                return View("ThemTopping");
            }

            try
            {
                int sanPhamId = IDSP ?? 0; // Gán IDSP là 0 nếu không được truyền vào
                string sql = $"EXEC sp_ThemTopping N'{TenTP}', {GiaTP}, {sanPhamId}";
                db.get(sql);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi thêm topping: " + ex.Message);
                return View("ThemTopping");
            }

            return RedirectToAction("Topping", "Admin");
        }

        [HttpPost]
        public ActionResult XoaTopping(string id)
        {
            ViewBag.id = db.get("EXEC sp_XoaTopping " + id + ";");
            return RedirectToAction("Topping", "Admin");
        }




        public ActionResult ChinhSuaTopping(int id)
        {
            // Lấy thông tin topping từ cơ sở dữ liệu
            var toppingData = db.get("SELECT * FROM TOPPING WHERE IDTP = " + id);

            // Kiểm tra dữ liệu trả về
            if (toppingData == null || toppingData.Count == 0)
            {
                return HttpNotFound(); // Trả về lỗi 404 nếu không tìm thấy
            }

            // Chuyển dữ liệu vào ViewBag
            ViewBag.topping = toppingData;

            return View();
        }

        [HttpPost]
        public ActionResult CapNhatTopping(int IDTP, string TenTP, float GiaTP, int IDSP)
        {
            if (!ModelState.IsValid)
            {
                return View("ChinhSuaTopping");
            }

            try
            {
                // Kiểm tra giá topping phải lớn hơn 0
                if (GiaTP <= 0)
                {
                    ModelState.AddModelError("", "Giá topping phải lớn hơn 0");
                    var toppingData = db.get("SELECT * FROM TOPPING WHERE IDTP = " + IDTP);
                    ViewBag.topping = toppingData;
                    return View("ChinhSuaTopping");
                }

                // Gọi stored procedure để cập nhật topping
                string sql = "EXEC sp_ChinhSuaTopping " +
                    "@IDTP = " + IDTP + ", " +
                    "@TenTP = N'" + TenTP + "', " +
                    "@GiaTP = " + GiaTP + ", " +
                    "@IDSP = " + IDSP;

                db.get(sql);
                return RedirectToAction("Topping", "Admin"); // Chuyển hướng về trang quản lý topping
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật topping: " + ex.Message);
                var toppingData = db.get("SELECT * FROM TOPPING WHERE IDTP = " + IDTP);
                ViewBag.topping = toppingData;
                return View("ChinhSuaTopping");
            }
        }*/
    }
}