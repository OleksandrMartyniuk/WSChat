var ws;
function Connect() {
    ws = new WebSocket("ws://localhost/WSChat/WSHandler.ashx");
    ws.onopen = function () {
        alert("Connected");
       // document.getElementById("Status").innerHTML = "Connected to server " + "(" + sessionStorage['username'] + ")";
    };
    ws.onmessage = function (evt) {
        Listen(evt.data); // Файл ResponseHandler.js
    };
    ws.onerror = function (evt) {
        document.getElementById("Status").innerHTML = evt.message;
    };
    ws.onclose = function () {
        alert("Disconnected");
    }
    //ws.onclose = function () {
    //    document.getElementById("Status").innerHTML = "Disconnected from server " + "(" + sessionStorage['username'] + ")";
    //};
}
