function submitAjax(options) {
    if (!options || !options.url) {
        console.error("submitAjax: url is required");
        return;
    }

    $.ajax({
        url: options.url,
        method: options.method || "POST",
        data: options.data || {},
        headers: options.headers || {},
        dataType: options.dataType || "json",
        success: function (response) {
            if (typeof options.success === "function") {
                options.success(response);
            }
        },
        error: function (xhr, status, error) {
            if (typeof options.error === "function") {
                options.error(xhr, status, error);
            } else {
                console.error("submitAjax error:", status, error);
            }
        }
    });
}

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
