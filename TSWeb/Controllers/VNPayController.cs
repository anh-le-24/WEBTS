using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using TSWeb.Models.VNPAY;
using TSWeb.Models;
using System.Security.Cryptography;
using System.Text;

namespace TSWeb.Controllers
{
    public class VNPayController : Controller
    {
        DatabaseModel db = new DatabaseModel();

        public ActionResult CreatePaymentUrl(string orderId, string amount, string orderInfo)
        {
            try
            {
                // Lấy thông tin cấu hình từ web.config
                string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"];
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"];
                string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"];
                string vnp_ReturnUrl = ConfigurationManager.AppSettings["vnp_ReturnUrl"];

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(orderId) || !int.TryParse(amount, out int vnp_Amount))
                {
                    return Content("Dữ liệu không hợp lệ.");
                }

                // VNPay yêu cầu số tiền là số nguyên và nhân 100 (VND)
                vnp_Amount *= 100;

                // Lấy địa chỉ IP của khách hàng
                string vnp_IpAddr = Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(vnp_IpAddr)) vnp_IpAddr = "127.0.0.1";

                // Tạo danh sách tham số gửi đến VNPay
                Dictionary<string, string> vnp_Params = new Dictionary<string, string>()
                {
                    { "vnp_Version", "2.1.0" },
                    { "vnp_Command", "pay" },
                    { "vnp_TmnCode", vnp_TmnCode },
                    { "vnp_Amount", vnp_Amount.ToString() },
                    { "vnp_CurrCode", "VND" },
                    { "vnp_TxnRef", orderId },
                    { "vnp_OrderInfo", orderInfo },
                    { "vnp_OrderType", "other" },
                    { "vnp_Locale", "vn" },
                    { "vnp_ReturnUrl", vnp_ReturnUrl },
                    { "vnp_IpAddr", vnp_IpAddr },
                    { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") }
                };

                // Tạo URL thanh toán với tham số
                VnPayLibrary vnpay = new VnPayLibrary();
                string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret, vnp_Params);

                return Redirect(paymentUrl);
            }
            catch (Exception ex)
            {
                return Content("Lỗi xử lý thanh toán: " + ex.Message);
            }
        }

        public static string HmacSHA512(string key, string inputData)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);

            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                return BitConverter.ToString(hashValue).Replace("-", "").ToUpper(); // Chuyển về viết hoa
            }
        }

        public ActionResult PaymentReturn()
        {
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"];
            var vnp_Params = Request.QueryString;
            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (string key in vnp_Params.AllKeys)
            {
                if (!string.IsNullOrEmpty(vnp_Params[key]) && key != "vnp_SecureHash")
                {
                    data.Add(key, vnp_Params[key]);
                }
            }

            // Sắp xếp tham số theo thứ tự A-Z
            string queryString = string.Join("&", data.OrderBy(x => x.Key).Select(x => x.Key + "=" + x.Value));

            // Debug: In thông tin queryString
            Console.WriteLine("QueryString: " + queryString);

            // Lấy vnp_SecureHash từ request
            string vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
            string signData = HmacSHA512(vnp_HashSecret, queryString);

            if (signData != vnp_SecureHash)
            {
                string orderId = Request.QueryString["vnp_TxnRef"];
                string amountRaw = Request.QueryString["vnp_Amount"];

                // Chuyển amount về số nguyên để so sánh đúng
                int amount = int.Parse(amountRaw) / 100;

                string responseCode = Request.QueryString["vnp_ResponseCode"];
                DateTime dateTime = DateTime.Now;
                string dateTH = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

                if (Session["taikhoan"] == null)
                {
                    ViewBag.Message = "Lỗi: Người dùng chưa đăng nhập!";
                    return View();
                }

                string taikhoan = Session["taikhoan"].ToString();

                if (responseCode == "00") // Thanh toán thành công
                {
                    string sqlCommand = $"EXEC ThemDonHang '{dateTime}', N'Đang xử lý', {amount}, {taikhoan}, '{dateTH}', N'Thanh toán qua ví VnPay'";
                    db.get(sqlCommand);
                    ViewBag.Message = "Thanh toán thành công!";
                }
                else
                {
                    ViewBag.Message = "Thanh toán thất bại!";
                }
            }
            else
            {
                ViewBag.Message = queryString;
            }
            return View();
        }
    }
}
