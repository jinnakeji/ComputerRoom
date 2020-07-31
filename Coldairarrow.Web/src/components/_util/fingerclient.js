
var _eventList = [];
var ws;

function FingerClient() {

}

/**
 * 链接服务器
 * @param {String} url 链接地址
 */
FingerClient.prototype.connectionServer = function (url) {
    ws = new WebSocket(url);

    ws.onopen = function (e) {
        _eventList.filter(x => x.type == "open").forEach(function (item) {
            console.log("fingerclient:opend");
            item.callback(e);
        });
    };

    ws.onclose = function (e) {
        _eventList.filter(x => x.type == "close").forEach(function (item) {
            console.log("fingerclient:closed");
            item.callback(e);
        });
    };

    ws.onmessage = function (e) {
        _eventList.filter(x => x.type == "message").forEach(function (item) {
            console.log("fingerclient:message");
            var data = JSON.parse(e.data);

            item.callback({
                source: e,
                data: data
            });
        });
    };
};

/**
 * 事件
 * @param {String} type message | open | close
 * @param {any} callback 回调事件
 */
FingerClient.prototype.on = function (type, callback) {
    _eventList.push({ type: type, callback: callback });
};

/**
 * 移除事件
 * @param {Function} callback 事件
 */
FingerClient.prototype.remove = function (callback) {
    var index = _eventList.findIndex(x => x.callback == callback);
    if (index >= 0) {
        _eventList.splice(index, 1);
    }
};

/**关闭 */
FingerClient.prototype.close = function () {
    this.ws.close(1000, "链接结束");
};

/**
 * 发送消息
 * @param {Object} data 消息
 */
FingerClient.prototype.send = function (data) {
    this.ws.send(JSON.stringify(data));
};

//开始监听
FingerClient.prototype.startMonitor = function () {
    ws.send(JSON.stringify({
        type: "start_monitor"
    }));
};

//停止监听
FingerClient.prototype.endMonitor = function () {
    ws.send(JSON.stringify({
        type: "end_monitor"
    }));
};


export default FingerClient;