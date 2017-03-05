var ws;
function connection() {
    if (ws === undefined) {

        ws = new WebSocket("ws://sanyok-001-site1.htempurl.com/WSHandler.ashx");//192.168.1.100 10.200.26.51

        ws.onopen = function() {
            sessionStorage['detailPage'] = true;
            if (sessionStorage['status'] === "loggin") {
                var req = new Request("Auth", "status", new Array(sessionStorage["username"], sessionStorage["password"]));
                ws.send(JSON.stringify(req));
            }
        };
        ws.onmessage = function(evt) {
            ResponseHandler.Handle(evt.data);
        };
        ws.onclose = function() {
            sessionStorage['detailPage'] = undefined;
            ws = undefined;
            ShowAuth();
            alert("Connection is closed...");
        };

        ws.SendMessage = function(msg) {
            if (ws === undefined) {
                alert("Connection is closed...");
                return;
            }
            ws.send(JSON.stringify(msg));
        };
    }
};

window.onload = function () {
    if (sessionStorage['status'] === "loggin") {
        ShowLobby();
    }
    else{
        ShowAuth();
    }
    connection();
}
