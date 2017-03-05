function statusChangeCallback(response) {
    if (response.status === 'connected') {
        testAPI();
    }
    else if (response.status === 'not_authorized') {
        alert('Please log ' +  'into this app.');
    }
    else {
        alert('Please log ' +  'into Facebook.');
    }
}

function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

window.fbAsyncInit = function () {
    FB.init({
        appId: '159651474545587',
        cookie: true,
        xfbml: true,
        version: 'v2.8'
    });
};

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "lib/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

function testAPI() {
    FB.api('/me?fields=id,first_name,email', 'get',
        function (response) {
            var req = new Request("Auth", "Facebook", response['first_name']);
            ws.SendMessage(req);

        });
}
