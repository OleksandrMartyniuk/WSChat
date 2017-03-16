$(document).ready(function () {
    
    var username = Cookies.get('username');
    var status = Cookies.get('status');
    $('#username').text(username);

    connection();
})