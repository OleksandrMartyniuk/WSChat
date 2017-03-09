function ResponseHandler() {

}

ResponseHandler.Handle = function (msg) {
    var req = JSON.parse(msg);
    
    switch (req.Module)
    {
        case "Lobby":
            switch (req.Cmd) {
                case "Notification": alert(req.Args); break;
            }
        case "admin":
            switch (req.Cmd)
            {
                case "ban":   UI.Ban();   break;
                case "unban": UI.Unban(); break;
            }
            break;
        case "info":
            if (req.Cmd == "all")
            {
                if (req.args.length == 0)
                {
                    return;
                }
                var roomList = new [];
                var a = req.args;
                for(var i=0; i< a.length; i++){
                    var robj = new roomObj();
                    robj.Name = (a[i])['Name'];
                    robj.clients = (a[i])['clients'];
                    roomList.push(robj);
                }
            }
            break;
        case "login":
            switch (req.Cmd)
            {
                case "ok":
                    if (response.Args !== undefined) {
                        sessionStorage['username'] = response.Args;
                        sessionStorage['status'] = 'loggin';
                    }
                    ShowLobby();
                break;
               
                case "admin":
                   // loggedAsAdmin?.Invoke((string)req.args);
                    break;
                case "banned":
                 //   loggedBanned?.Invoke((string)req.args);
                    break;
                case "Forgot":
                    switch (response.Args) {
                        case "Success":
                            alert("password sent by email")
                            break;
                        case "Error":
                            alert("No such user exists. Please make sure that you entered your login");
                            break;
                    }
                    break;
                default:
                 //   loginFail?.Invoke((string)req.args);
                    break;
            }
            break;
        case "msg":
            switch (req.Cmd)
            {
                case "msg":
                    Messages.onRoomMessageReceived( req.args[0], new ChatMessage( req.args[1]));
                    break;
                case "active":
                    var roomName = req.args[0];
                    var msgList = req.args[1];
                    for(var i=0; i<msgList.length; i++){
                        msgList[i] = new ChatMessage([msgList[i]]);
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
                    Rooms.userEntered(req.args[0], req.args[1]);
                    break;
                case "left":
                    Rooms.userLeft(req.args[0], req.args[1]);
                    break;
            }
            break;
        case "private":
            onPrivateMessageReceived(new ChatMessage(req.args));
            break;
        case "room":
            switch (req.Cmd)
            {
                case "created":
                    Rooms.addRoomToMenu(req.args);////////
                    break;
                case "removed":
                    Rooms.RemoveRoom(req.args);
                    break;
                default:
                    alert(req.args);
                    break;
            }
            break;
        case "history":
            switch (req.Cmd)
            {
                case "room":
                    var roomName = req.args[0];
                    var msgList = req.args[1];
                    for(var i=0; i<msgList.length; i++){
                        msgList[i] = new ChatMessage([msgList[i]]);
                        if(msgList[i].Sender == sessionStorage['username']){
                            msgList[i].Sender = 'Me';
                        }
                    }
                    Messages.OnHistoryReceived(roomName, msgList);
                    break;
                case "private":
                    var userName = req.args[0] == sessionStorage['username']? req.args[1]: req.args[0];
                    var msgList = req.args[2];
                    for(var i=0; i<msgList.length; i++){
                        msgList[i] = new ChatMessage([msgList[i]]);
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
