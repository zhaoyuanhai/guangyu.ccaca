(function () {
    angular.module("app")
        .filter("totext", function () {
            return function (text) {
                if (!text)
                    return text;

                return $("<p>" + text + "</p>").text();
            }
        })
        .filter("length", function () {
            return function (text, length) {
                if (!text)
                    return text;

                if (text.length <= length)
                    return text;
                return text.substr(0, length) + "...";
            }
        });
})();