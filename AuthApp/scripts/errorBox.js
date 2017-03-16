function showMessage(message) {
    $('#errorBox span').html(message);
    $('#errorBox').removeClass('alert-success');
    $('#errorBox').addClass('alert-danger');
    $('#errorBox').removeClass('collapse');
}

function messageSuccess(message) {
    $('#errorBox span').html(message);
    $('#errorBox').removeClass('alert-danger');
    $('#errorBox').addClass('alert-success');
    $('#errorBox').removeClass('collapse');
}

function hideMessage() {
    $('#errorBox').addClass('collapse');
}