document.addEventListener('DOMContentLoaded', function () {
    $("a").each(function (index, element) {
        var href = element.href;
        if (href.indexOf("returnUrl") < 0) {
            element.href = "http://localhost:55624/account/login?returnUrl=" + href;
        }

    });
}, false);