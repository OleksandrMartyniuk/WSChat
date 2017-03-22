var ws;
function connect(key) {
    if (ws === undefined) {

        //ws = new WebSocket("ws://127.0.0.1:9898");
        ws = new WebSocket("ws://localhost/WSChat/WSHandler.ashx");//192.168.1.100 10.200.26.51

        ws.onopen = function () {
            
            RequestManager.Login(key)
        };
        ws.onmessage = function (evt) {
            console.log(evt.data);
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

function ShowAuth() {
    
}
