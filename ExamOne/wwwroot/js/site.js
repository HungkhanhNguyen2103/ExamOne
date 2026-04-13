let isDev = false;

if (!isDev) {
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

    document.addEventListener('keydown', function (e) {
        if (
            e.key === "F12" ||
            (e.ctrlKey && e.shiftKey && e.key === "I") ||
            (e.ctrlKey && e.shiftKey && e.key === "J") ||
            (e.ctrlKey && e.key === "U")
        ) {
            e.preventDefault();
            return false;
        }
    });

    const channel = new BroadcastChannel("exam_channel");

    let isDuplicate = false;

    channel.postMessage("CHECK");

    channel.onmessage = (e) => {
        if (e.data === "CHECK") {
            channel.postMessage("EXISTS");
        }

        if (e.data === "EXISTS") {
            isDuplicate = true;
            alert("Bạn đang mở nhiều tab!");
            window.close();
        }
    };

    //console.log('123');

    document.addEventListener('contextmenu', function (e) {
        e.preventDefault();
    });

    //let devtoolsOpen = false;

    //setInterval(function () {
    //    const threshold = 160;
    //    if (
    //        window.outerWidth - window.innerWidth > threshold ||
    //        window.outerHeight - window.innerHeight > threshold
    //    ) {
    //        if (!devtoolsOpen) {
    //            devtoolsOpen = true;

    //            // 👉 xử lý khi phát hiện mở DevTools
    //            alert("DevTools detected!");

    //            // redirect
    //            window.location.href = "/";

    //            // hoặc thử đóng tab (không phải lúc nào cũng work)
    //            window.close();
    //        }
    //    } else {
    //        devtoolsOpen = false;
    //    }
    //}, 1000);
}
