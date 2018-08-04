function parseQuery(queryString) {
    var query = {};
    var pairs = (queryString[0] === '?' ? queryString.substr(1) : queryString).split('&');
    for (var i = 0; i < pairs.length; i++) {
        var pair = pairs[i].split('=');
        query[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1] || '');
    }
    return query;
}

$(function () {
    debugger;

    var queryParams = parseQuery(window.location.search);

    var state = queryParams["state"];
    var code = queryParams["code"];

    var savedState = localStorage.getItem("state-google");

    if (state != savedState || !code) {
        alert("Problem...");
        window.location.href = "/";
    }

    $.ajax({
        url: apiServer + "auth/google",
        data: {
            code: code,
        }
    })
        .done(response => {
            var jwtToken = response.Token;
            saveToken(jwtToken);

            window.location.href = "/";
        })
        .fail(); // DYI
});