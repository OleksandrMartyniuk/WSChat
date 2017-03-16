$(document).ready(function () {

    var mail = $('#email');

    $('#login_btn').click(function () {
        window.location.replace(host + "LoginForm.html");
    });

    $('#send_btn').click(function () {

        mail.parent().removeClass('has-error has-feedback');

        var eFlag = validateEmail(mail.val());

        if (!eFlag) {
            mail.parent().addClass('has-error has-feedback');
            showMessage('<p>invalid email<p>');
            return;
        }

        var status = getPassword(mail.val(), function (status) {
            switch (status) {
                case 'not_found':
                    mail.parent().addClass('has-error has-feedback');
                    showMessage('Email ' + mail.val() + ' not found');
                    break;
                case 'OK':
                    mail.parent().removeClass('has-error has-feedback');
                    messageSuccess('<p>The password was successfully sent to you</p>');
                    break;
                default: break;
            }
        });
    });
});

