var ws;
function Connect() {
    ws = new WebSocket("ws://localhost/WSChat/WSHandler.ashx");
    ws.onopen = function () {
        alert("Connected");
    };

    ws.onerror = function (evt) {
        alert(evt.message);
    };

    ws.onclose = function () {
        alert("Disconnected");
    }
}
