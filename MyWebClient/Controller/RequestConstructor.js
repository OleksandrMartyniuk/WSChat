function requestManager() {
};

requestManager.SendMessage = function (message, roomName) {

    var msg = new ChatMessage();
    msg.Sender = sessionStorage['username'];
    msg.Text = message;
    msg.TimeStamp = new Date();
    var reqObj = new RequestObject("msg", "msg", [ roomName, msg ] );
    ws.SendMessage(JSON.stringify(reqObj));
}

requestManager.SetActiveRoom = function (roomName) {

    var reqObj = new RequestObject("msg", "active", roomName);
    ws.SendMessage(JSON.stringify(reqObj));
}

requestManager.LeaveRoom = function (roomName) {

    var reqObj = new RequestObject("msg", "leave", roomName);
    ws.SendMessage(JSON.stringify(reqObj));
}

requestManager.CreateRoom = function (roomName) {

    var reqObj = new RequestObject("room", "create", roomName);
    ws.SendMessage(JSON.stringify(reqObj));
}

requestManager.CloseRoom = function (roomName) {

    var reqObj = new RequestObject("room", "close", roomName);
    ws.SendMessage(JSON.stringify(reqObj));
}

requestManager.RequestData = function () {

    var reqObj = new RequestObject("info", "get", null);
    ws.SendMessage(JSON.stringify(reqObj));
}

requestManager.RequestMessageList = function (room, timestamp) {
    if (!timestamp) {
        timestamp = new Date();
    }
    var reqObj = new RequestObject("history", "room", [ room, timestamp]);
    ws.SendMessage(JSON.stringify(reqObj));
}

requestManager.RequestPrivateMessageList = function (username, timestamp) {
    if (!timestamp) {
        timestamp = new Date();
    }
    var reqObj = new RequestObject("history", "private", [sessionStorage['username'], username, timestamp]);
    ws.SendMessage(JSON.stringify(reqObj));
}

requestManager.SendPrivateMessage = function (username, msg) {

    var reqObj = new RequestObject("private", username, msg);
    ws.SendMessage(JSON.stringify(reqObj));
}

requestManager.AdminBan = function (username, exp) {

    var reqObj = new RequestObject("admin", "ban", [username, exp]);
    ws.SendMessage(JSON.stringify(reqObj));
}

requestManager.AdminBanPermanent = function (username, msg) {

    var reqObj = new RequestObject("admin", "ban", username);
    ws.SendMessage(JSON.stringify(reqObj));
}
