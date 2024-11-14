function updateTotalPrice() {
    let totalPrice = 0;

    // Get the price of the selected product
    let selectedProduct = document.getElementById("idsp");
    let productPrice = parseFloat(selectedProduct.options[selectedProduct.selectedIndex].getAttribute("data-price"));

    // Check if the product price is valid
    if (isNaN(productPrice)) {
        console.error('Giá sản phẩm không hợp lệ');
        return;
    }

    // Get the quantity of the product
    let quantity = parseInt(document.getElementById('SoLuong').value);
    if (isNaN(quantity) || quantity <= 0) {
        console.error('Số lượng không hợp lệ');
        return;
    }

    // Calculate total price for the product (product price x quantity)
    totalPrice = productPrice * quantity;

    // Add selected topping prices
    document.querySelectorAll('input[name="toppings"]:checked').forEach(function (topping) {
        let toppingPrice = parseFloat(topping.getAttribute('data-price'));
        if (!isNaN(toppingPrice)) {
            totalPrice += toppingPrice * quantity;
        }
    });

    // Add price for size if 'L' is selected (extra 10,000)
    let selectedSize = document.querySelector('input[name="size"]:checked');
    if (selectedSize) {
        let sizePrice = selectedSize.value === 'L' ? 10000 : 0;
        totalPrice += sizePrice * quantity;
    }

    // Get the selected ice and sugar options (these do not affect the price)
    let selectedIce = document.querySelector('input[name="ice"]:checked');
    let selectedSugar = document.querySelector('input[name="sugar"]:checked');

    // Format and update the total price in the input field
    let formattedPrice = Math.round(totalPrice); // Round the total price to the nearest integer
    document.getElementById('TongTien').value = formattedPrice;

    // Update the hidden input field "SelectedOptions" with chosen options
    getSelectedOptions();
}

// Function to update selected options (toppings, ice, sugar) in a hidden input field
function getSelectedOptions() {
    let selectedOptions = [];

    // Get selected ice option
    document.querySelectorAll('input[name="ice"]:checked').forEach(function (ice) {
        selectedOptions.push(ice.value);
    });

    // Get selected sugar option
    document.querySelectorAll('input[name="sugar"]:checked').forEach(function (sugar) {
        selectedOptions.push(sugar.value);
    });

    // Get selected toppings
    document.querySelectorAll('input[name="toppings"]:checked').forEach(function (topping) {
        selectedOptions.push(topping.value);
    });

    // Update hidden input with the selected options as a comma-separated string
    document.getElementById('SelectedOptions').value = selectedOptions.join(', ');
}

// Function to update quantity based on user action (plus or minus)
function updateQuantity(action) {
    let quantityInput = document.getElementById('SoLuong');
    let currentQuantity = parseInt(quantityInput.value);

    if (action === 'plus' && currentQuantity < 10) {
        quantityInput.value = currentQuantity + 1;
    } else if (action === 'minus' && currentQuantity > 1) {
        quantityInput.value = currentQuantity - 1;
    }

    updateTotalPrice(); // Recalculate total price when quantity changes
}

// Ensure total price updates immediately when quantity changes directly
document.getElementById('SoLuong').addEventListener('change', updateTotalPrice);
