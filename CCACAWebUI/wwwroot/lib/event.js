function MyEvent() {

}

MyEvent.prototype.add = function (key, fn) {
    if (!(this[key] instanceof Array)) {
        this[key] = new Array();
    }

    this[key].push(fn);
}

MyEvent.prototype.emit = function (key, props) {
    if (this[key] instanceof Array) {
        for (var i = 0; i < this[key].length; i++) {
            this[key][i](props);
        }
    }
}

MyEvent.prototype.remote = function (key, fn) {

}

MyEvent.prototype.remoteAll = function (key) {

}