function DoLogin() {
    var login = document.getElementById("login").value;
    var password = document.getElementById("password").value;
    if (login == undefined || login.length < 3) {
        alert("Некорректный логин");
        return;
    }
    else if (password == undefined || password.length < 3) {
        alert("Некорректный пароль");
        return;
    }
    else {     
       // OnLoginSuccessfull(sessionStorage['username']);
       // document.getElementById("AuthDiv").style.display = "none";
        //document.getElementById("MainDiv").style.display = "block";
        Login(login, password);
    }
}

function DoLogout() {
    if (sessionStorage['username'] != undefined) {
        document.getElementById("AuthDiv").style.display = "block";
        document.getElementById("MainDiv").style.display = "none";
        document.getElementById("password").value = "";
        Logout(sessionStorage['username']);
        ws.close();
        ClearAll();
    }
}

function DoSetActiveRoom() {
    var rooms = document.getElementById("rooms");
    var selectedOption = rooms.options[rooms.selectedIndex];
    var roomname = selectedOption.value;
    selectedOption.style.fontWeight = "bolder";
    selectedOption.style.color = "green";
    if (roomname != undefined) {
        sessionStorage['activeRoom'] = roomname;
        CreateUserList(Rooms[roomname]);
        SetActiveRoom(roomname);
        for (var i = 0; i < rooms.options.length; i++) 
            if(rooms.options[i].value != sessionStorage['activeRoom'])
            {
                rooms.options[i].style.fontWeight = "normal";
                rooms.options[i].style.color = "black";
            }            
        document.getElementById("ActiveRoom").innerHTML = roomname ;
    } else
        return;
}

function DoCreateRoom() {
    var roomname = document.getElementById("roomname").value;
    if (roomname != undefined && roomname != "" && roomname.length > 0 && roomname != ' ') {
        alert("Room " + roomname + " created");
        CreateRoom(roomname);
        document.getElementById("roomname").value = "";
    } else
        return;
}

function DoCloseRoom() {
    if (sessionStorage['activeRoom'] != undefined)
        CloseRoom(sessionStorage['activeRoom']);
    else
        return;
}

function DoSendMessage() {
    var message = document.getElementById("message").value;
    if (message != "" && sessionStorage['activeRoom'] != undefined) {
        if (message.indexOf("@") == 0) {
            message = message.trim();
            message = message.substring(1);
            var tmp = message.split(':');
            var receiver = tmp[0];
            var msg = tmp[1].trim();
            var privateMsg = "Me --> " + receiver + ": " + msg;
            PrintMessage(privateMsg, true);
            SendPrivateMessage(msg, receiver);                                
        } else {
            var publicMsg = "Me: " + message;
            PrintMessage(publicMsg, false);
            SendMessage(message, sessionStorage['activeRoom']);          
        }
    } else
        return;
}

function SelectMessageReceiver() {
    var receiverName = document.getElementById("users").options[document.getElementById("users").selectedIndex].value;
    document.getElementById("message").value = "@" + receiverName + ": ";
    document.getElementById("message").focus();
}

function PrintMessage(message, type) {
    alert("Print" + message);
    document.getElementById("message").value = "";
    var d = new Date();
    var time = "[" + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds() + "]";
    var msg = time + message;
    if(type)
        document.getElementById("privateMessagesTextArea").innerHTML += msg + "\n";
    else
        document.getElementById("messagesTextArea").innerHTML += msg + "\n";
}

function ClearAll() {
    document.getElementById("message").value = "";
    document.getElementById("privateMessagesTextArea").innerHTML = "";
    document.getElementById("messagesTextArea").innerHTML = "";
    document.getElementById("rooms").innerHTML = "";
    document.getElementById("users").innerHTML = "";
    document.getElementById("ActiveRoom").innerHTML = "";
    sessionStorage.clear();
}

function CreateUserList(userList) {
    var users = document.getElementById("users");
    users.innerHTML = "";
    for (var i = 0; i < userList.length; i++) {
        var option = document.createElement("option");
        option.appendChild(document.createTextNode(userList[i]));
        option.setAttribute("value", userList[i]);
        option.addEventListener("dblclick", SelectMessageReceiver);
        users.appendChild(option);
    }
}

function CreateRoomList(roomList) {
    var rooms = document.getElementById("rooms");
    rooms.innerHTML = "";
    for (var key in roomList) {
        var option = document.createElement("option");
        option.appendChild(document.createTextNode(key));
        option.setAttribute("value", key);
        option.addEventListener("dblclick", DoSetActiveRoom);
        rooms.appendChild(option);
    }
}

