// Write your JavaScript code.
(function () {
    $(".menu .h-list li:not(.move)").hover(function () {
        move(this);
    });

    $(".menu .h-list").mouseleave(function () {
        var ele = $(".menu .h-list>li.active");
        move(ele[0]);
    });

    function move(ele) {
        $(".menu>.h-list>.move").css({
            left: ele.offsetLeft - 10,
            width: $(ele).width() + 20
        });
    }

    move($(".menu li.active")[0]);
})();