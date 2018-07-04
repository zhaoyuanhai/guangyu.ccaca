var myEvent = new MyEvent();
$(function () {

    window.myEvent = myEvent;
    function renderPage() {
        var body = $(document.body);
        var width = body.width();
        var height = body.height();
        var headHeight = $("#head").height();

        if (width < 1200) width = 1200;
        else width = "auto";

        if (height < 600) height = 600;

        $(".container").css('width', width);
        $("#one-page").css({
            width: width,
            height: height - headHeight
        });
        myEvent.emit("resize", [width, height]);
    }
    renderPage();
    $(window).resize(renderPage);

    //首页大图动画效果
    $(window).scroll(function () {
        var se = document.documentElement.clientHeight;
        $(".relt").each(function () {
            var top = this.getBoundingClientRect().top;
            if (top < se) {
                $(this).addClass("bounceInUp");
            }
        });
    });

    $(".error-placehoder").hide();
    $("#myForm [type='button']").click(function () {
        var url = $("#myForm").attr("action");
        var model = $("#myForm").serialize();

        var _self = this;
        var text = $(_self).val();

        $(_self).attr("disabled", true);
        $(_self).val(text + "...");
        $.post(url, model, function (result) {
            if (result.success) {
                location.reload();
            } else {
                $(_self).removeAttr("disabled");
                $(_self).val(text)
                $(".error-placehoder").show();
                $(".error-placehoder .error").text(result.msg);
            }
        });
    });
});

/**
 * 是否显示登录框
 * @param {boolean} isShow
 * @param {string} returnUrl
 */
function Login(isShow, returnUrl) {
    if (isShow) {
        $(".login-container").show();
        if (returnUrl) {
            var form = $(".login-container").find("form");
            var r = /ReturnUrl=([/\w]+(&|$))/i;
            var result = null;

            var act = decodeURIComponent(form.attr("action"));
            if (r.test(act)) {
                result = act.replace(r, "ReturnUrl=" + returnUrl + "&");
            } else {
                if (act.indexOf('?') != -1)
                    result = act + "&ReturnUrl=" + returnUrl;
                else
                    result = act + "?ReturnUrl=" + returnUrl;
            }

            form.attr("action", result);
        }
    } else {
        $(".login-container").hide()
    }
}
