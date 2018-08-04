var apiServer;

var token;
var rawToken;
var isLoggedIn;

function makeid() {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < 15; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
}

function getToken() {
    return localStorage.getItem("jwt");
}

function saveToken(token) {
    localStorage.setItem("jwt", token);
}

function login() {
    var state = makeid();
    localStorage.setItem("state-google", state);

    $.ajax({
        url: apiServer + "auth/google-auth",
        data: {
            state: state,
        }
    })
        .done(response => {
            window.location.href = response.Url;
        })
        .fail(xhr => alert("Dumb server"));
}

function logout() {
    localStorage.removeItem("jwt");
    window.location.href = "/";
}

function requireLogin() {
    if (!isLoggedIn) {
        login();
        return false;
    } else {
        return true;
    }
}

$(function () {
    apiServer = $("[data-api-server]").attr("data-api-server");

    rawToken = getToken();

    if (rawToken) {
        token = jwt_decode(rawToken);

        if (token) {
            isLoggedIn = true;
        }
    }

    if (isLoggedIn) {
        $(".logout-panel").removeClass("d-none");
        $("[data-user-email]").html(token.email);

        $(".btn-logout").click(logout);
    } else {
        $(".login-panel").removeClass("d-none");

        $(".btn-login").click(login);
    }
});
