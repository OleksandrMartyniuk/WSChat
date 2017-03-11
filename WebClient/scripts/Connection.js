var ws;
function Connect() {
    ws = new WebSocket("ws://localhost/WSChat/WSHandler.ashx");
    ws.onopen = function () {
        alert("Connected");
       // document.getElementById("Status").innerHTML = "Connected to server " + "(" + sessionStorage['username'] + ")";
    };

    ws.onerror = function (evt) {
        alert(evt.message);
    };

    ws.onclose = function () {
        alert("Disconnected");
    }

    ws.onclose = function () {
        document.getElementById("Status").innerHTML = "Disconnected from server " + "(" + sessionStorage['username'] + ")";
    };
}
