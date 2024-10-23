let signinBtn = document.querySelector(".signinBtn");
let signupBtn = document.querySelector(".signupBtn");
let body = document.querySelector("body");
signupBtn.onclick = function () {
    body.classList.add("slide");
};
signinBtn.onclick = function () {
    body.classList.remove("slide");
};
function validateForm() {
    // Kiểm tra email có @gmail.com
    const email = document.getElementById("email").value;
    const emailPattern = /^[a-zA-Z0-9._%+-]+@gmail\.com$/;
    if (!emailPattern.test(email)) {
        alert("Email phải là @gmail.com");
        return false;
    }

    // Kiểm tra mật khẩu: phải có ít nhất 8 ký tự, 1 ký tự đặc biệt, 1 chữ cái viết hoa
    const password = document.getElementById("password").value;
    const passwordPattern = /^(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]).{8,}$/;
    if (!passwordPattern.test(password)) {
        alert("Mật khẩu phải có ít nhất 8 ký tự, 1 chữ cái viết hoa và 1 ký tự đặc biệt");
        return false;
    }

    // Kiểm tra hai mật khẩu có trùng khớp
    const confirmPassword = document.getElementById("confirmPassword").value;
    if (password !== confirmPassword) {
        alert("Mật khẩu và xác nhận mật khẩu không khớp");
        return false;
    }

    // Kiểm tra số điện thoại có hợp lệ
    const phone = document.getElementById("phone").value;
    if (phone.length < 10 || phone.length > 11) {
        alert("Số điện thoại phải có 10 hoặc 11 số");
        return false;
    }

    return true;
}