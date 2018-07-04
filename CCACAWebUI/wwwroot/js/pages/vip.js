function Links() {
    var cons = $(".linkcons");
    var items = $("ul", cons);
    var width = 569;
    var currentIndex = 0;

    items.each(function (index, ele) {
        $(ele).css({ left: (index * width) + "px" });
    });

    this.next = function () {
        if (currentIndex == items.length - 1)
            return;

        currentIndex += 1;
        this.run();
    }

    this.prev = function () {
        if (currentIndex == 0)
            return;

        currentIndex -= 1;
        this.run();
    }

    this.run = function () {
        $(".lf,.rf").unbind();
        var self = this;
        items.each(function (index, ele) {
            (function (index) {
                $(ele).animate({ left: (width * index + (-currentIndex * width)) + "px" }, "slow", function () {
                    if (index == items.length - 1) {
                        $(".lf").click(self.prev.bind(self));
                        $(".rf").click(self.next.bind(self));
                    }
                });
            })(index);
        });
    }
}

var links = new Links();
$(".lf").click(links.prev.bind(links));
$(".rf").click(links.next.bind(links));