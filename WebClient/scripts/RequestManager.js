function Login(login, password) {
    if (login != undefined && password != undefined) {
        sessionStorage['username'] = login;
        sessionStorage['password'] = password;
        Connect();
        var request = {
            Module: "login",
            Cmd: "in",
            args: [
                login,
                password
            ]
        };
        ws.onopen = () => ws.send(JSON.stringify(request));      
    }
}

function Logout(name) {
    if (name != undefined) {
        var request = {
            Module: "logout",
            Cmd: "",
            args: name
        };
        sessionStorage['username'] = undefined;
        sessionStorage['password'] = undefined;
        ws.send(JSON.stringify(request));
    }
}

function SetActiveRoom(roomname) {
    if (roomname != undefined) {

        var request = {
            Module: "msg",
            Cmd: "active",
            args: roomname
        };
        ws.send(JSON.stringify(request));
    }
}

function CreateRoom(roomname) {
    if (roomname != undefined) {
        var request = {
            Module: "room",
            Cmd: "create",
            args: roomname
        };
        ws.send(JSON.stringify(request));
    }
}

function CloseRoom(roomname) {
    if (roomname != undefined) {
        var request = {
            Module: "room",
            Cmd: "close",
            args: roomname
        };
        ws.send(JSON.stringify(request));
    }
}

function SendMessage(message, roomname) {
    if (message != undefined && roomname != undefined && sessionStorage['username'] != undefined) {
        var d = new Date();
        var time = "[" + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds() + "]";
        var request = {
            Module: "msg",
            Cmd: "msg",
            args: [
                roomname,
                sessionStorage['username'],
                message,
                time
            ]
        };
        ws.send(JSON.stringify(request));
      //  alert(JSON.stringify(request));
    }
}

function SendPrivateMessage(message, username) {
    if (message != undefined && username != undefined) {
        var d = new Date();
        var time = "[" + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds() + "]";
        var request = {
            Module: "private",
            Cmd: username,
            args: [
                username,
                message,
                time
            ]
        };
        ws.send(JSON.stringify(request));
       // alert(JSON.stringify(request));
    }
}

