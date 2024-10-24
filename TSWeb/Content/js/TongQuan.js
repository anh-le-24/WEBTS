document.addEventListener('DOMContentLoaded', function () {
    // Giả lập dữ liệu thống kê
    const totalOrders = 100;
    const totalRevenue = 4500000.00;
    const totalCustomers = 100;

    // Hiển thị dữ liệu thống kê
    document.getElementById('total-orders').innerHTML = totalOrders;
    document.getElementById('total-revenue').innerHTML = totalRevenue.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
    document.getElementById('total-customers').innerHTML = totalCustomers;

    // Dữ liệu cho biểu đồ doanh thu
    const revenueLabels = ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6'];
    const revenueData = {
        labels: revenueLabels,
        datasets: [{
            label: 'Doanh thu',
            data: [1200000, 1500000, 2000000, 2500000, 3000000, 4500000],
            borderColor: 'rgba(75, 192, 192, 1)',
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderWidth: 1
        }]
    };

    // Dữ liệu cho biểu đồ khách hàng
    const customerData = {
        labels: revenueLabels,
        datasets: [{
            label: 'Số lượng khách hàng',
            data: [10, 20, 30, 40, 50, 60],
            borderColor: 'rgba(153, 102, 255, 1)',
            backgroundColor: 'rgba(153, 102, 255, 0.2)',
            borderWidth: 1
        }]
    };

    // Dữ liệu cho biểu đồ đơn hàng
    const orderData = {
        labels: revenueLabels,
        datasets: [{
            label: 'Số đơn hàng',
            data: [5, 15, 25, 35, 45, 55],
            borderColor: 'rgba(255, 159, 64, 1)',
            backgroundColor: 'rgba(255, 159, 64, 0.2)',
            borderWidth: 1
        }]
    };

    // Tạo biểu đồ doanh thu
    const ctxRevenue = document.getElementById('revenueChart').getContext('2d');
    const revenueChart = new Chart(ctxRevenue, {
        type: 'line', // Bạn có thể thay đổi loại biểu đồ nếu cần
        data: revenueData,
        options: {
            responsive: true,
            title: {
                display: true,
                text: 'Doanh thu'
            },
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    // Tạo biểu đồ khách hàng
    const ctxCustomer = document.getElementById('customerChart').getContext('2d');
    const customerChart = new Chart(ctxCustomer, {
        type: 'bar', // Bạn có thể thay đổi loại biểu đồ nếu cần
        data: customerData,
        options: {
            responsive: true,
            title: {
                display: true,
                text: 'Số lượng khách hàng'
            },
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    // Tạo biểu đồ đơn hàng
    const ctxOrder = document.getElementById('orderChart').getContext('2d');
    const orderChart = new Chart(ctxOrder, {
        type: 'bar', // Bạn có thể thay đổi loại biểu đồ nếu cần
        data: orderData,
        options: {
            responsive: true,
            title: {
                display: true,
                text: 'Số đơn hàng'
            },
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
});