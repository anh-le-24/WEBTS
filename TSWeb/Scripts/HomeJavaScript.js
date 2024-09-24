﻿let currentIndex = 0;
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