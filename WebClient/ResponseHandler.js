var ws;

function Connect() {
    ws = new WebSocket("ws://localhost/WSChat/WSHandler.ashx");
    ws.onopen = function () {
        document.getElementById("Status").innerHTML = "Connected";
    };
    ws.onmessage = function (evt) {
        ProcessRequest(evt.data);
    };
    ws.onerror = function (evt) {
    };
    ws.onclose = function () {
        document.getElementById("Status").innerHTML = "Disconnected";
    };
}

function ProcessResponse(request) {
    var req = JSON.parse(request);
    switch (req.module) {
        case 'login':
            switch (req.cmd) {
                case 'ok':
                    break;
                case 'admin':
                    break;
                case 'banned':
                    break;
                default:
                    break;
            }
            break;
        case 'msg':
            switch (req.cmd) {
                case 'msg':
                    break;
                case 'active':
                    break;
                case 'notify':
                    break;
                case 'entered':
                    break;
                case 'left':
                    break;
            }
            break;
        case 'admin':
            switch (req.cmd) {
                case 'ban':
                    break;
                case 'unban':
                    break;
            }
            break;
        case 'info':
            if (req.Cmd == 'all') {

            }
            break;
        case 'private':
            break;
        case 'room':
            switch (req.Cmd) {
                case 'created':
                    break;
                case 'removed':
                    break;
                default:
                    break;
            }
            break;
        default:
            break;
    }
}


