
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

