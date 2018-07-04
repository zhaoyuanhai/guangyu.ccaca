$("#commit").click(function () {
    if (nullOrEmpty($("#userName").val())) {
        alert("用户名必填");
        return false;
    }

    if (nullOrEmpty($("#email").val())) {
        alert("邮箱必填");
        return false;
    }

    if (nullOrEmpty($("#phone").val())) {
        alert("手机号必填");
        return false;
    }

    if (nullOrEmpty($("#realName").val())) {
        alert("姓名必填");
        return false;
    }

    if (nullOrEmpty($("#password").val())) {
        alert("密码必填");
        return false;
    }

    if (nullOrEmpty($("#repassword").val())) {
        alert("密码必填");
        return false;
    }

    if ($("#password").val() != $("#repassword").val()) {
        alert("两次密码输入不一致");
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