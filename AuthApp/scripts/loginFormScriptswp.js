$(document).ready(function () {

    $('a#passwordRecoveryLink').attr('href', host + 'PasswordRecoveryForm.html');

    var lgn = $('#login');
    var pass = $('#pwd');

    

    $('#register_btn').click(function () {
        window.location.replace(host + "Registration.html");
    });

    $('#login_btn').click(function () {

        hideMessage();

        lgn.parent().removeClass('has-error has-feedback');
        pass.parent().removeClass('has-error has-feedback');

        var lFlag = validateUsername(lgn.val());
        var pFlag = validatePassword(pass.val());

        if (!lFlag && !pFlag) {
            lgn.parent().addClass('has-error has-feedback');
            pass.parent().addClass('has-error has-feedback');

            showMessage('<p>Login should be at least 3 digits letters or numbers </p><p>Password should be at least 3 digits letters or numbers</p>');
            return;
        }
        else if(!lFlag) {
            lgn.parent().addClass('has-error has-feedback');
            showMessage('Login should be at least 3 digits letters or numbers');
            return;
        }
        else if(!pFlag) {
            pass.parent().addClass('has-error has-feedback');
            showMessage('Password should be at least 3 digits letters or numbers');
            return;
        }

        var lgnVal = lgn.val();
        var status = login(lgnVal, pass.val(), function(status){
            switch (status) {
                case 'login':
                    lgn.parent().addClass('has-error has-feedback');
                    showMessage('Username ' + lgnVal + ' doesnt exist');
                    break;
                case 'password':
                    pass.parent().addClass('has-error has-feedback');
                    showMessage('Wrong password');
                    break;
            }
        });
    });
});