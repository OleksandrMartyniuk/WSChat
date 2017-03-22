function ResponseHandler() {

}

ResponseHandler.Handle = function (msg) {
    var req = JSON.parse(msg);
    
    switch (req.Module)
    {
        case "admin":
            switch (req.Cmd)
            {
                case "ban":
                    UI.Ban();
                    sessionStorage['role'] = 'banned';
                    alert("You got banned till " + req.Args);
                    break;
                case "unban":
                    UI.Unban();
                    sessionStorage['role'] = 'user';
                    alert("You got unbanned");
                    break;
            }
            break;
        case "info":
            if (req.Cmd == "all")
            {
                if (req.Args.length == 0)
                {
                    return;
                }
                var roomList = [];
                var a = req.Args;
                for(var i=0; i< a.length; i++){
                    var robj = new roomObj();
                    robj.Name = (a[i])['Name'];
                    robj.clients = (a[i])['clients'];
                    robj.creator = (a[i])['Creator'];
                    roomList.push(robj);
                }
                Rooms.OnDataReceived(roomList);
            }
            break;
        case "login":
            switch (req.Cmd)
            {
                case "admin":
                    if (req.Args !== undefined) {
                        sessionStorage['username'] = req.Args;
                        $('#username').text(req.Args);
                        RequestManager.RequestData();
                        sessionStorage['role'] = 'admin';
                    }
                    break;
                case "user":
                    if (req.Args !== undefined) {
                        sessionStorage['username'] = req.Args;
                        $('#username').text(req.Args);
                        RequestManager.RequestData();
                        sessionStorage['role'] = 'user';
                    }
                break;
                   // loggedAsAdmin?.Invoke((string)req.Args);
                case "banned":
                    if (req.Args !== undefined) {
                        sessionStorage['username'] = req.Args;
                        $('#username').text(username);
                        sessionStorage['role'] = 'banned';
                    }
                    UI.Ban();
                 //   loggedBanned?.Invoke((string)req.Args);
                    break;
                default:
                    alert(req.Args);
                    break;
            }
            break;
        case "msg":
            switch (req.Cmd)
            {
                case "msg":
                    var msg = new ChatMessage(req.Args[1]);
                    Messages.onRoomMessageReceived( req.Args[0], msg);
                    break;
                case "active":
                    var roomName = req.Args[0];
                    var msgList = req.Args[1];
                    for(var i=0; i<msgList.length; i++){
                        msgList[i] = new ChatMessage(msgList[i]);
                        if(msgList[i].Sender == sessionStorage['username']){
                            msgList[i].Sender = 'Me';
                        }
                    }
                    Messages.OnHistoryReceived(roomName, msgList);
                    break;
                case "notify":
                    //
                    break;
                case "entered":
                    Rooms.userEntered(req.Args[0], req.Args[1]);
                    break;
                case "left":
                    Rooms.userLeft(req.Args[0], req.Args[1]);
                    break;
            }
            break;
        case "private":
            Messages.onPrivateMessageReceived(new ChatMessage(req.Args));
            break;
        case "room":
            switch (req.Cmd)
            {
                case "created":
                    var r = req.Args;
                    var room = new roomObj(r.Name, r.creator, r.clients? r.clients: []);
                    Rooms.addRoomToMenu(room);
                    break;
                case "removed":
                    Rooms.removeRoom(req.Args);
                    break;
                default:
                    alert(req.Args);
                    break;
            }
            break;
        case "history":
            switch (req.Cmd)
            {
                case "room":
                    var roomName = req.Args[0];
                    var msgList = req.Args[1];
                    for(var i=0; i<msgList.length; i++){
                        msgList[i] = new ChatMessage(msgList[i]);
                        if(msgList[i].Sender == sessionStorage['username']){
                            msgList[i].Sender = 'Me';
                        }
                    }
                    Messages.OnHistoryReceived(roomName, msgList);
                    break;
                case "private":
                    var userName = req.Args[0] == sessionStorage['username']? req.Args[1]: req.Args[0];
                    var msgList = req.Args[1];
                    for(var i=0; i<msgList.length; i++){
                        msgList[i] = new ChatMessage(msgList[i]);
                        if(msgList[i].Sender == sessionStorage['username']){
                            msgList[i].Sender = 'Me';
                        }
                    }
                    Messages.OnHistoryReceived(userName, msgList, 'true');
                    break;
                default:
                    break;
            }
            break;
    }
}
