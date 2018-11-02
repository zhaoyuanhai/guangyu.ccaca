$(function () {
    var pageIndex = 0;
    var isLoad = false;

    $("#setyw").click(function () {
        if (isLoad) {
            return;
        }
        isLoad = true;
        $.getJSON("/GetInformation", { pageIndex: ++pageIndex }, function (result) {
            if (result.isLastPage) {
                pageIndex = 0;
            }

            if (result.pageCount === 1) {
                return;
            }

            var container = $(".yw");
            container.children().remove();
            $.each(result.datas, function (index, item) {
                var li =
                    '<li>\
                        <span class="title">@title</span >\
                        <span class="item">@content</span>\
                        <span class="date">@date</span>\
                    </li >';

                li = li.replace("@title", item.title)
                    .replace("@content", $("<div>" + item.content + "</div>").text())
                    .replace("@date", item.createTime.split('T')[0]);

                container.append(li);
            });
            isLoad = false;
        });
    });

    $("#top").click(function () {
        $('html,body').animate({ scrollTop: '0px' }, 800);
    });

    $('.limarquee').liMarquee({
        direction: 'up',
        drag: false,
        scrollamount: 30
    });
});