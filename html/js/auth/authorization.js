function Authorization(response) {
    switch (response.Cmd) {
        case "LogIn":
            if (response.Args !== undefined) {
                sessionStorage['username'] = response.Args;
                sessionStorage['status'] = 'loggin';
            }
            ShowLobby();
            break;
        case "Forgot": Forgot(response.Args); break;
    }
}

function Forgot(args) {
    switch(args){
        case "Success":
            alert("password sent by email")
            break;
        case "Error":
            alert("No such user exists. Please make sure that you entered your login");
            break;
    }
}

function auth(login, password) {
    ws.sendMessage(new Request("Auth", "LogIn", new Array(login, password)));
}

function logout() {
    ws.sendMessage(new Request("Auth", "LogOut", document.getElementById("textLogin").value));
    sessionStorage['username'] = undefined;
    sessionStorage['password'] = undefined;
    sessionStorage['status'] = undefined;
    ShowAuth();
}

function inspection(login, password) {
    if (ws === undefined) {
        alert("Connection is closed...");
        return false;
    }
    if (~login.indexOf(" ") || ~password.indexOf(" ") ) {
        alert("take away spaces!");
        return false;
    }
    if (login == "" && password == "") {
        alert("Fill all fields!")
        return false;
    }
    return true;
}

function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}
function inspectionReg(login, password, email) {
    if (ws === undefined) {
        alert("Connection is closed...");
        return false;
    }
    if (validateEmail(email) == false) {
        alert("Поле Email должно быть в формате example@exmp.com");
        return false;
    }
    if (~login.indexOf(" ") || ~password.indexOf(" ") || ~email.indexOf(" ")) {
        alert("take away spaces!");
        return false;
    }
    if (login == "" && password == "" && email=="") {
        alert("Fill all fields!")
        return false;
    }
    return true;
}
function login() {
    var login = document.getElementById("textLogin").value;
    var password = document.getElementById("textPassword").value;
    connection();
    if(inspection(login, password)==true){
        sessionStorage['username'] = login;
        sessionStorage['password'] = password;
        auth(login, password);
    }
}

function registr() {
   
    var login = document.getElementById("textLogin").value;
    var password = document.getElementById("textPassword").value;
    var email = document.getElementById("textEmail").value;
    if (inspectionReg(login, password, email) == true){
        var req = new Request("Auth", "Registration", new Array(login, password, email));
        ws.sendMessage(req);
    }
}

function forget(login) {
    
    if (~login.indexOf(" ") || login == "") {
        alert("Fill Login field");
        return;
    }
    ws.sendMessage(new Request("Auth", "Forget", login));
}