function clientsList() {
    var req = new Request("Lobby", "refreshClients", "null");
    ws.sendMessage(req);
}

function OnInvite() {
    var player = GetSelectedPlayer();
    var req = new Request("HandShake", "Invite", new Array(player, "XO"));
    ws.sendMessage(req);
}

function goPlaying(play) {
    var req = new Request("HandShake", "Ok", new Array(play[0], "XO"));
    ws.sendMessage(req);
}

function move(args) {  
    var req = new Request("Game", "Move", args);
    ws.sendMessage(req);
}
function RemoveClient(args) {
    var req = new Request("Game", "Remove", args);
    ws.sendMessage(req);
}
function start(args) {
    var req = new Request("Game", "Start", args);
    ws.sendMessage(req);
}

