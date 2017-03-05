function RequestManager() {
};

RequestManager.SendMessage = function (message, roomName) {
    var msg = new ChatMessage();
    msg.Sender = sessionStorage['username'];
    msg.Text = message;
    msg.TimeStamp = new Date();
    ws.SendMessage(new RequestObject("msg", "msg", [ roomName, msg ] ));
}

RequestManager.SetActiveRoom = function (roomName) {
    ws.SendMessage(new RequestObject("msg", "active", roomName));
}

RequestManager.LeaveRoom = function (roomName) {
    ws.SendMessage(new RequestObject("msg", "leave", roomName));
}

RequestManager.CreateRoom = function (roomName) {
    ws.SendMessage(new RequestObject("room", "create", roomName));
}

RequestManager.CloseRoom = function (roomName) {
    ws.SendMessage(new RequestObject("room", "close", roomName));
}

RequestManager.RequestData = function () {
    ws.SendMessage(new RequestObject("info", "get", "null"));
}

RequestManager.RequestMessageList = function (room, timestamp) {
    if (!timestamp) {
        timestamp = new Date();
    }
    ws.SendMessage(new RequestObject("history", "room", [ room, timestamp]));
}

RequestManager.RequestPrivateMessageList = function (username, timestamp) {
    if (!timestamp) {
        timestamp = new Date();
    }
    ws.SendMessage(new RequestObject("history", "private", [sessionStorage['username'], username, timestamp]));
}

RequestManager.SendPrivateMessage = function (username, msg) {
    ws.SendMessage(new RequestObject("private", username, msg));
}

RequestManager.AdminBan = function (username, exp) {
    ws.SendMessage(new RequestObject("admin", "ban", [username, exp]));
}

RequestManager.AdminBanPermanent = function (username, msg) {
    ws.SendMessage(new RequestObject("admin", "ban", username));
}
