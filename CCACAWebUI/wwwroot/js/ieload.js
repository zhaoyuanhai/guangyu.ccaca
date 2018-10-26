$(function () {
    function isIE() { //ie?
        if (!!window.ActiveXObject || "ActiveXObject" in window)
            return true;
        else
            return false;
    }

    function getLink(href) {
        var link = document.createElement("link");
        link.rel = "stylesheet";
        link.href = href;
        document.head.appendChild(link);
    }

    if (isIE()) {
        getLink("/css/pages/index-ie.css");
    }
});