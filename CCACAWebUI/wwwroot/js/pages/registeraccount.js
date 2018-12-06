$("#commit").click(function () {
    if (nullOrEmpty($("#userName").val())) {
        alert(__("用户名必填"));
        return false;
    }

    if (nullOrEmpty($("#email").val())) {
        alert(__("邮箱必填"));
        return false;
    }

    if (nullOrEmpty($("#phone").val())) {
        alert(__("手机号必填"));
        return false;
    }

    if (nullOrEmpty($("#realName").val())) {
        alert(__("姓名必填"));
        return false;
    }

    if (nullOrEmpty($("#password").val())) {
        alert(__("密码必填"));
        return false;
    }

    if (nullOrEmpty($("#repassword").val())) {
        alert(__("密码必填"));
        return false;
    }

    if ($("#password").val() != $("#repassword").val()) {
        alert(__("两次密码输入不一致"));
        return false;
    }

    return true;
});

function nullOrEmpty(str) {
    if (str == null) {
        return true;
    }

    if (str.trim() == "") {
        return true;
    }

    return false;
}