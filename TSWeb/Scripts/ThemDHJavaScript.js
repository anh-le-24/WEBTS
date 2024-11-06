function updatePrice() {
    var productSelect = document.getElementById("IDSP");
    var selectedOption = productSelect.options[productSelect.selectedIndex];
    var price = selectedOption.getAttribute("data-price");
    document.getElementById("Gia").value = price; // Cập nhật giá vào input
    updateTotalPrice(); // Cập nhật tổng giá
}

function updateTotalPrice() {
    var quantity = parseInt(document.getElementById("SoLuong").value) || 0; // Số lượng
    var productPrice = parseFloat(document.getElementById("Gia").value) || 0; // Giá sản phẩm
    var toppingPrice = 0; // Tổng giá topping
    var sizePrice = 0; // Giá thêm cho size L

    // Kiểm tra kích thước
    var selectedSize = document.getElementById("Size").value;
    if (selectedSize === "L") {
        sizePrice = 10000; // Cộng thêm 10.000 nếu chọn size L
    }

    // Lặp qua các topping được chọn
    var toppingCheckboxes = document.querySelectorAll('input[name="toppings"]:checked');
    toppingCheckboxes.forEach(function (checkbox) {
        toppingPrice += parseFloat(checkbox.getAttribute("data-price")) || 0; // Cộng giá topping
    });

    // Tính tổng tiền
    var totalPrice = (productPrice + toppingPrice + sizePrice) * quantity; // Tổng tiền
    document.getElementById("TongTien").value = totalPrice; // Cập nhật tổng tiền
}