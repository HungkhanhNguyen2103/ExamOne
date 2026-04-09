document.addEventListener("DOMContentLoaded", () => {
    const defaultImg = "/img/user.jpg";

    document.querySelectorAll("img").forEach(img => {

        img.onerror = function () {
            if (this.src.endsWith(defaultImg)) return;

            this.src = defaultImg;
        };

        if (img.complete && img.naturalWidth === 0) {
            img.src = defaultImg;
        }
    });
});