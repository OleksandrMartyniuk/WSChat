$(document).ready(function () {
    
    var key = Cookies.get('access-key');
    var host = 'http://localhost/AuthApp/';

    connect(key);

    $('#btnLogOut').click(function () {
        window.location.replace(host + "LoginForm.html");
    })
})