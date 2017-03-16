var host = 'http://localhost/AuthApp/';
var server = 'http://localhost/WSChat/';
//"http://xzxzxz-001-site1.htempurl.com/Login.ashx"

function login(login, password, callbackFunction) {
    var r = new XMLHttpRequest();
    r.open("POST", host + "Login.ashx", true); 

    var req = new Object();
    req.login = login;
    req.password = password;
    r.send(JSON.stringify(req));
    r.onreadystatechange = function () {
        if (r.readyState != 4)
            return;
        var response = r.responseText;
        switch (response) {
            case "ok":
                var key = r.getResponseHeader('access-key');
                window.location.replace(server + 'ClientLoadHandler.ashx?key='+key);
                break;
            default:
                callbackFunction(response);
                break;
        }
    }
}

function register(email, login, password, callbackFunction) {
    var r = new XMLHttpRequest();
    r.open("POST", host + "Registration.ashx", true);
    var robj = new Object();
    robj.email = email;
    robj.login = login;
    robj.password = password;

    r.send(JSON.stringify(robj));
    r.onreadystatechange = function () {
        if (r.readyState != 4)
            return;

        callbackFunction(r.responseText);
    }
}

function getPassword(email, callBackFunction) {
    var r = new XMLHttpRequest();
    r.open("POST", host + "PasswordRecovery.ashx", true);

    r.send(email);
    r.onreadystatechange = function () {
        if (r.readyState != 4)
            return;

        callBackFunction(r.responseText);
    }
}

function validateUsername(usrname) {
    var regex = /(\w{3,})/;
    return regex.test(usrname);
}

function validateEmail(email) {
    var regex = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|ua|ru|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum)\b/;
    return regex.test(email);
}

function validatePassword(pwd) {
    var regex = /(\w{3,})/;
    return regex.test(pwd);
}