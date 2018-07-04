(function (window) {
    function Vessel(id) {

        this.window = null;

        //初始化
        function init() {
            var hash = location.hash;
            let sharpSymbolIndex = hash.indexOf('#');
            let queSymbolIndex = hash.indexOf('?');

            let route, args;

            if (sharpSymbolIndex != -1) {
                if (queSymbolIndex != -1) {
                    route = hash.substr(1, queSymbolIndex);
                    args = hash.substr(queSymbolIndex);
                } else {
                    route = hash.substr(1);
                }
            }

            if (route) {
                if (id) {
                    window.frames[id].addEventListener("load", loadWindow);
                    window.frames[id].src = route;
                    this.window = window.frames[id].contentWindow;
                } else {
                    this.window = window.frames[0].contentWindow;
                }
            } else {

            }
        }

        //加载window
        function loadWindow() {
            let contentLocation = this.contentWindow.location;
            var contentPath = contentLocation.pathname + contentLocation.search;
            location.hash = contentPath;
            window.frames[id].style.display = "block";
        }

        init.bind(this)();
    }

    window.Vessel = Vessel;
})(window);