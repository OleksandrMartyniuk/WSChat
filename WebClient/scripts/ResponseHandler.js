var clients;
var Rooms;

function Listen(response) {
    var req = JSON.parse(response);
    switch (req.Module) {
        case 'login':
            switch (req.Cmd) {
                case 'ok':
                    OnLoginSuccessfull(req.args);
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
            switch (req.Cmd) {
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
            switch (req.Cmd) {
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

function OnLog(username) {
    sessionStorage['username'] = username;
    document.getElementById("AuthDiv").style.display = "none";
    document.getElementById("MainDiv").style.display = "block";
    Rooms = {};
    Rooms['Host'] = new Array("Vasya", "Karina", "Arnold", "Omar", "Sasha");
    Rooms['Room1'] = new Array("Igor", "Kristina");
    CreateRoomList(Rooms);
}

function OnLoginSuccessfull(username) {   
    OnLog(username);
}