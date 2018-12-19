function like(username, id) {
    //Possible problem if there is another likebtn with the same id
    let likeButton = $(`#likeBtn`);
    let baseUrl = "http://localhost:5000/api/posts";
    let data = {
        'username': username,
        'postId': id,
    };
    let likesParagraph = $(`#likes${id}`);

    if (!likeButton.hasClass('active')) {
        likeButton.addClass('active');

        $.ajax({
            url: baseUrl + "/AddLike",
            method: "POST",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            data: JSON.stringify(data),
            success: function () {
                let likesText = likesParagraph.text();
                likesParagraph.text(`${+likesText + 1}`)
            },
            error: function (msg) {
                console.dir(msg);
            }
        });

    }
    else {
        likeButton.removeClass('active');
        $.ajax({
            url: baseUrl + "/RemoveLike",
            method: "POST",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                'Content-Type': "application/json",
            },
            data: JSON.stringify(data),
            success: function () {
                let likesText = likesParagraph.text();
                likesParagraph.text(`${+likesText - 1}`);
            },
            error: function (msg) {
                console.dir(msg);
            }
        });
    }
}