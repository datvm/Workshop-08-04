$(function () {
    if (!requireLogin()) {
        return;
    }

    $.ajax({
        url: apiServer + "tweet",
        headers: {
            "Authorization": "Bearer " + rawToken,
        }
    })
        .done(response => {
            var template = $("#template-tweet").html();
            var list = $("#lst-tweets");
            list.html("");

            for (var tweet of response) {
                var dom = $(template);

                dom.find("[data-title]").html(tweet.Title);
                dom.find("[data-content]").html(tweet.Content);
                dom.find("[data-created-time]").html(tweet.CreatedTime);

                list.append(dom);
            }

        })
        .fail(xhr => alert(""));
});