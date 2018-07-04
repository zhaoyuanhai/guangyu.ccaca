window.myEvent.add("resize", function () {
    var wheight = $(window).height();
    var hheight = $("#head").height();
    $("#main-body").height(wheight - hheight);
});
