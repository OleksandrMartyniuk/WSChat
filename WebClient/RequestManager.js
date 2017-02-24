var ws;
var username;

function Connect() {
    ws = new WebSocket("ws://localhost/WSChat/WSHandler.ashx");
    ws.onopen = function () {
        document.getElementById("Status").innerHTML = "Connected";
        ws.send(Login('v1', '1'));
    };
    ws.onmessage = function (evt) {
    };
    ws.onerror = function (evt) {
        document.getElementById("Status").innerHTML = evt.message;
    };
    ws.onclose = function () {
        document.getElementById("Status").innerHTML = "Disconnected";
    };
}

function GetRequestObject(module, cmd, args) {
    var req = {};
    req.module = module;
    req.cmd = cmd;
    req.args = args;
    return req;
}

function Login(name, password) {
    var args = [name, password];
    var req = GetRequestObject("login", "in", args);
    ws.send(JSON.stringify(req));
}

function Logout(name) {
    var req = GetRequestObject("logout", null, name);
    ws.send(JSON.stringify(req));
}

/*function SendMessage(msg, room) {
    var date = new Date();
    var time = "[" + date.getHours() + ":" + date.getMinutes + ":" + 
    var chatMessage = {
        Sender: username,
        Text: msg,

    }
    var args = {

    }
    var req = GetRequestObject("msg", "msg", );
}*/



