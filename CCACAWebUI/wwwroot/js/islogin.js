$(".isLogin").click(function () {
    var il = false;
    var url = $(this).attr("href");
    $.ajax({
        url: apihref,
        async: false,
        type: "get",
        success: function (data) {
            il = data;
        }
    });

    if (il === false) {
        Login(true, url);
        return false;
    } else {
        return true;
    }
});