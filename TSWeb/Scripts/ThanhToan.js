// Confirmation before submitting the order
function confirmOrder() {
    let confirmMessage = "Bạn chắc chắn muốn đặt hàng?";

    // Show a confirmation dialog
    if (confirm(confirmMessage)) {
        return true;  // Continue submitting the form
    } else {
        return false;  // Prevent form submission
    }
}

// Hàm tính tổng tiền của các sản phẩm
function calculateTotalAmount() {
    let totalAmount = 0;
    let productPrices = document.querySelectorAll('.product-price');
    productPrices.forEach(function (priceElement) {
        let price = parseInt(priceElement.textContent.replace(' VNĐ', '').replace(',', '').trim());
        totalAmount += price;
    });
    document.getElementById('totalAmount').value = totalAmount;
    updateShippingCost(document.querySelector('input[name="shippingspeed"]:checked')?.value || 'quick');
}

// Hàm cập nhật giá trị phí vận chuyển và tính toán tổng cộng
function updateShippingCost(shippingType) {
    let totalAmount = parseInt(document.getElementById('totalAmount').value) || 0;
    let shippingCost = parseInt(document.getElementById('phivanchuyen').textContent.replace('đ', '').trim()) || 0;
    let khuyenMai = parseInt(document.getElementById('khuyenmai').value) || 0;
    let loyaltyPointsUsed = parseInt(document.getElementById('loyaltyPointsUsed').value) || 0;

    // Kiểm tra loại vận chuyển và gán phí vận chuyển tương ứng
    if (shippingType === 'quick') {
        shippingCost = 5000;
    } else if (shippingType === 'slow') {
        shippingCost = 2000;
    }

    // Hiển thị phí vận chuyển
    document.getElementById('phivanchuyen').textContent = shippingCost + 'đ';

    // Tính tổng cộng
    let finalTotal = totalAmount + shippingCost - khuyenMai - loyaltyPointsUsed;

    // Đảm bảo tổng cộng không âm
    document.getElementById('finalTotal').value = finalTotal < 0 ? 0 : finalTotal;
}


// Hàm thêm khuyến mãi
function addPromoCode() {
    let promoValue = prompt("Nhập giá trị khuyến mãi:"); // Ví dụ nhập khuyến mãi
    let khuyenMai = parseInt(promoValue);

    if (khuyenMai && khuyenMai > 0) {
        document.getElementById('khuyenmai').value = khuyenMai;
        updateShippingCost(document.querySelector('input[name="shipping_speed"]:checked')?.value || 'quick');
    }
}

// Hàm áp dụng voucher và tính toán số tiền giảm
function applyVoucher(discountPercent, minOrderAmount, voucherId, expiryDate, startDate) {
    let totalAmount = parseInt(document.getElementById('totalAmount').value);

    // Kiểm tra ngày hiện tại và ngày bắt đầu
    let now = new Date();
    let voucherExpiryDate = new Date(expiryDate);
    let voucherStartDate = new Date(startDate);

    // Nếu voucher chưa đến ngày bắt đầu
    if (voucherStartDate > now) {
        alert("Voucher chưa đến ngày áp dụng. Vui lòng chọn voucher khác.");
        return; // Không cho phép áp dụng voucher
    }

    // Kiểm tra xem voucher có hết hạn không
    if (voucherExpiryDate < now) {
        alert("Voucher đã hết hạn. Vui lòng chọn voucher khác.");
        return; // Không cho phép áp dụng voucher đã hết hạn
    }

    // Kiểm tra xem voucher có đủ điều kiện được áp dụng không
    if (totalAmount >= minOrderAmount) {
        let discountAmount = Math.floor((totalAmount * discountPercent) / 100);

        // Cập nhật giá trị khuyến mãi vào trường hợp ẩn
        document.getElementById('khuyenmai').value = discountAmount;
        document.getElementById('promoCode').value = discountAmount + ' VNĐ';

        // Lưu voucher ID vào input ẩn
        document.getElementById('voucherId').value = voucherId;

        // Tính lại tổng tiền
        updateShippingCost(document.querySelector('input[name="shippingspeed"]:checked')?.value || 'quick');

        // Đóng form voucher
        toggleVoucherForm();

        alert("Voucher đã được áp dụng thành công!");
    } else {
        alert("Không đủ điều kiện để áp dụng voucher. Tổng tiền đơn hàng phải từ " + minOrderAmount + " VND trở lên.");
    }
}

// Hàm thêm logic điểm tích lũy
function handleLoyaltyPoints(points) {
    let checkbox = document.getElementById('useLoyaltyPoints');
    let loyaltyPointsUsed = document.getElementById('loyaltyPointsUsed'); // Input ẩn để gửi giá trị về server

    // Nếu checkbox được chọn
    if (checkbox.checked) {
        loyaltyPointsUsed.value = points; // Cập nhật giá trị điểm tích lũy được sử dụng
    } else {
        loyaltyPointsUsed.value = 0; // Không sử dụng điểm tích lũy
    }

    // Cập nhật tổng tiền mà không thay đổi loại vận chuyển
    let currentShippingType = document.querySelector('input[name="shippingspeed"]:checked')?.value || 'quick';
    updateShippingCost(currentShippingType); // Truyền loại vận chuyển hiện tại
}

// Toggle voucher form display
function toggleVoucherForm() {
    let voucherForm = document.getElementById("voucherForm");
    voucherForm.style.display = (voucherForm.style.display === "none" || voucherForm.style.display === "") ? "flex" : "none";
}

window.onload = function () {
    calculateTotalAmount();
    document.getElementById('quick').checked = true; // Mặc định chọn vận chuyển nhanh
    updateShippingCost('quick'); // Áp dụng vận chuyển nhanh ngay khi tải trang
};

// Hàm mở modal VNPay
function openVNPayModal() {
    let finalTotalInput = document.getElementById("finalTotal");
    let orderInfoInput = document.getElementById("orderInfo");
    let vnpayLink = document.getElementById("vnpayLink");
    let vnpayModal = document.getElementById("vnpayModal");

    if (!finalTotalInput || !orderInfoInput || !vnpayLink || !vnpayModal) {
        alert("Lỗi: Một số phần tử không tồn tại!");
        return;
    }

    let orderId = Date.now(); // Tạo mã đơn hàng theo timestamp
    let amount = finalTotalInput.value.trim();
    let orderInfo = orderInfoInput.value.trim() || "Không có ghi chú";

    // Chuyển amount sang số nguyên
    let amountInt = Number(amount);

    if (!amount || isNaN(amountInt) || amountInt <= 0) {
        alert("Số tiền không hợp lệ!");
        return;
    }

    // Cập nhật link thanh toán VNPay
    let vnpayUrl = `/VNPay/CreatePaymentUrl?orderId=${orderId}&amount=${amountInt}&orderInfo=${encodeURIComponent(orderInfo)}`;
    vnpayLink.setAttribute("href", vnpayUrl);

    // Hiển thị modal nếu tồn tại
    vnpayModal.style.display = "block";
}

function closeVNPayModal() {
    vnpayModal.style.display = "none";
}

document.addEventListener('click', function (event) {
    if (event.target.matches('#menu, #menu1')) {
        const sideBar = document.querySelector('.side-bar');
        const overlaySidebar = document.querySelector('.overlay-sidebar');
        if (sideBar && overlaySidebar) {
            sideBar.classList.toggle('active');
            overlaySidebar.classList.toggle('active');
        }
    }

    if (event.target.matches('.overlay-sidebar')) {
        document.querySelector('.side-bar').classList.remove('active');
        document.querySelector('.overlay-sidebar').classList.remove('active');
    }
});