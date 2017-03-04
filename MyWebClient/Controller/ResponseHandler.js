function ResponseHandler() {

}

ResponseHandler.Handle = function (msg) {

    var req = new RequestObject(msg["Module"], msg["Cmd"], msg["args"]);

    switch (req.Module)
    {
        case "admin":
            switch (req.Cmd)
            {
                case "ban":
                    UI.Ban();
                    break;
                case "unban":
                    UI.Unban();
                    break;
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
                    loginSuccessfull?.Invoke((string)req.args);
                    break;
                case "admin":
                    loggedAsAdmin?.Invoke((string)req.args);
                    break;
                case "banned":
                    loggedBanned?.Invoke((string)req.args);
                    break;
                default:
                    loginFail?.Invoke((string)req.args);
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





/*
RequestObject req = JsonConvert.DeserializeObject<RequestObject>(Json);

            switch (req.Module)
            {
                case "admin":
                    switch (req.Cmd)
                    {
                        case "ban":
                            Banned?.Invoke();
                            break;
                        case "unban":
                            Unbanned?.Invoke();
                            break;
                    }
                    break;
                case "info":
                    if (req.Cmd == "all")
                    {
                        RoomObj[] rooms = JsonConvert.DeserializeObject<RoomObj[]>(req.args.ToString());
                        if (rooms.Length > 0)
                        {
                            roomDataReceived(rooms);//JsonConvert.DeserializeObject<RoomObj[]>((string)req.args));
                        }
                    }
                    break;
                case "login":
                    switch (req.Cmd)
                    {
                        case "ok":
                            loginSuccessfull?.Invoke((string)req.args);
                            break;
                        case "admin":
                            loggedAsAdmin?.Invoke((string)req.args);
                            break;
                        case "banned":
                            loggedBanned?.Invoke((string)req.args);
                            break;
                        default:
                            loginFail?.Invoke((string)req.args);
                            break;
                    }
                    break;
                case "msg":
                    switch (req.Cmd)
                    {
                        case "msg":
                            object[] args = JsonConvert.DeserializeObject<object[]>(req.args.ToString());
                            messageRecieived?.Invoke((string)args[0], JsonConvert.DeserializeObject<ChatMessage>(args[1].ToString()));
                            //messageRecieived?.Invoke(JsonConvert.DeserializeObject<ChatMessage>(req.args.ToString()));
                            break;
                        case "active":
                            args = JsonConvert.DeserializeObject<object[]>(req.args.ToString());
                            msgDataReceived?.Invoke((string)args[0], JsonConvert.DeserializeObject<ChatMessage[]>(args[1].ToString()));
                            break;
                        case "notify":
                            notificationReceived?.Invoke((string)req.args);
                            break;
                        case "entered":
                            args = JsonConvert.DeserializeObject<string[]>(req.args.ToString());
                            UserEntered?.Invoke((string)args[0], (string)args[1]);
                            break;
                        case "left":
                            args = JsonConvert.DeserializeObject<string[]>(req.args.ToString());
                            UserLeft?.Invoke((string)args[0], (string)args[1]);
                            break;
                    }
                    break;
                case "private":
                    privateMessageReceived?.Invoke(JsonConvert.DeserializeObject<ChatMessage>(req.args.ToString()));
                    break;
                case "room":
                    switch (req.Cmd)
                    {
                        case "created":
                            roomCreated?.Invoke((string)req.args);
                            break;
                        case "removed":
                            roomRemoved?.Invoke((string)req.args);
                            break;
                        default:
                            roomError?.Invoke((string)req.args);
                            break;
                    }
                    break;
                case "history":
                    switch (req.Cmd)
                    {
                        case "room":
                            object[] args = JsonConvert.DeserializeObject<object[]>(req.args.ToString());
                            ChatMessage[] history = JsonConvert.DeserializeObject<ChatMessage[]>(args[1].ToString());
                            RoomHistoryReceived?.Invoke((string)args[0], history);
                            break;
                        case "private":
                            args = JsonConvert.DeserializeObject<object[]>(req.args.ToString());
                            string user = (string)args[1] == Client.Username ? (string)args[1] : (string)args[0];
                            history = JsonConvert.DeserializeObject<ChatMessage[]>(args[2].ToString());
                            RoomHistoryReceived?.Invoke(user, history);
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }

*/