let currentIndex = 0;
let autoSlideInterval;

function moveSlides(n) {
    const slides = document.querySelectorAll(".slide");
    currentIndex = (currentIndex + n + slides.length) % slides.length;
    showSlides();
}

function currentSlide(n) {
    currentIndex = n - 1;
    showSlides();
}

function showSlides() {
    const slides = document.querySelector(".slides");
    slides.style.transform = `translateX(-${currentIndex * 100}%)`;

    const dots = document.querySelectorAll(".dot");
    dots.forEach(dot => dot.classList.remove("active"));
    dots[currentIndex].classList.add("active");
}

//chuyển silder sau mỗi 3s
function autoSlide() {
    autoSlideInterval = setInterval(() => {
        moveSlides(1); // Tự động chuyển đến slide tiếp theo
    }, 3000); // Chuyển slide mỗi 3 giây
}

// Hàm khởi tạo slider và bắt đầu auto-slide
function initSlider() {
    showSlides();
    autoSlide();
}

function stopAutoSlide() {
    clearInterval(autoSlideInterval);
}

function resumeAutoSlide() {
    autoSlide();
}

initSlider();

const slider = document.querySelector('.slider');
slider.addEventListener('mouseenter', stopAutoSlide);
slider.addEventListener('mouseleave', resumeAutoSlide);




/*Side-bar */
const overlaySidebar = document.querySelector('.overlay-sidebar');
const navBarBtn = document.querySelector('#menu');
const sideBar = document.querySelector('.side-bar');
navBarBtn.addEventListener('click', function () {
    sideBar.classList.toggle('active')
    overlaySidebar.classList.toggle('active')
});
console.log(navBarBtn);

const navBarBtn1 = document.querySelector('#menu1');
navBarBtn1.addEventListener('click', function () {
    sideBar.classList.toggle('active')
    overlaySidebar.classList.toggle('active')
});

overlaySidebar.addEventListener('click', function () {
    sideBar.classList.remove('active')
    overlaySidebar.classList.remove('active')
});

/*Chuyển động ưu đãi */
const btnLeft = document.querySelector("#btn-left");
const btnRight = document.querySelector("#btn-right");
const saleWrap = document.querySelector("#sale_wrap");
const saleItem = document.querySelectorAll(".sale__item");
const totalIndex = Math.ceil(saleItem.length - 3);
let currentIndexx = 0;
const widthWrap = saleWrap.clientWidth;
const changeImage = () => {
    if (currentIndexx > totalIndex) {
        currentIndexx = 0;
    } else if (currentIndexx < 0) {
        currentIndexx = totalIndex;
    }
    saleWrap.style.transform = `translateX(${(-widthWrap / 3) * currentIndexx}px)`;
};

btnRight.addEventListener("click", () => {
    currentIndexx++;
    changeImage();
});
btnLeft.addEventListener("click", () => {
    currentIndexx--;
    changeImage();
});