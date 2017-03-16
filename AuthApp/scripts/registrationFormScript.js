$(document).ready(function () {

    var lgn = $('#login');
    var pass = $('#pwd');
    var mail = $('#email');

    lgn.attr('placeholder-ok', lgn.attr('placeholder'));
    pass.attr('placeholder-ok', pass.attr('placeholder'));
    mail.attr('placeholder-ok', mail.attr('placeholder'));

    $('#login_btn').click(function () {
        window.location.replace(host + "LoginForm.html");
    });

    $('#register_btn').click(function () {

        lgn.parent().removeClass('has-error has-feedback');
        pass.parent().removeClass('has-error has-feedback');
        mail.parent().removeClass('has-error has-feedback');

        
        var lFlag = validateUsername(lgn.val());
        var pFlag = validatePassword(pass.val());
        var eFlag = validateEmail(mail.val());

        var message = "";

        if (!eFlag) {
            mail.parent().addClass('has-error has-feedback');
            message += '<p>invalid email<p>';
        }
        if (!lFlag) {
            lgn.parent().addClass('has-error has-feedback');
            message += '<p>Login should be at least 3 digits letters or numbers<p>';
        }
        if (!pFlag) {
            pass.parent().addClass('has-error has-feedback');
            message += '<p>Password should be at least 3 digits letters or numbers<p>';
        }
        if (!lFlag || !pFlag || !eFlag) {
            showMessage(message);
            return;
        }

        var lgnVal = lgn.val();
        var mailVal = mail.val();
        var status = register(mailVal, lgnVal, pass.val(), function (status) {
            switch (status) {
                case 'login':
                    lgn.parent().addClass('has-error has-feedback');
                    showMessage('Username ' + lgnVal + 'already exists');
                    break;
                case 'email':
                    mail.parent().addClass('has-error has-feedback');
                    showMessage('Email ' + mailVal + 'already exists');
                    break;
                case 'login&email':
                    mail.parent().addClass('has-error has-feedback');
                    lgn.parent().addClass('has-error has-feedback');
                    showMessage('Record alredy exists. Try password recovery');
                    break;
                case 'ok':
                    messageSuccess('<p>Congratulations! Your account successfuly created!<p><h4>Redirecting...</h4>');
                    setTimeout(window.location.replace(host + "LoginForm.html"), 500);
                    break;
            }
        });
    });
});

