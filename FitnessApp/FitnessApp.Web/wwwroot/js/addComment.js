function comment(username, id, profilePicture) {
    let baseUrl = "http://localhost:5000/api/comments"
    let textBox = $('#commentsTextBox');
    let comment = textBox.val();
    let data = {
        'content': comment,
        'username': username,
        'postId': id,
    };

    $.ajax({
        url: baseUrl + '/AddComment',
        method: "POST",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(data),
        success: function () {
            let div = $('<div>').addClass('col-md-12 m-1');
            let aTag = $('<a>').attr('href', baseUrl + `users/Profile/${username}`)
            aTag.text(username);
            let image = '<img src=\"' + profilePicture + '\" width="45px" class="img-rounded"/>';
            div.append(image);
            div.append(aTag);
            div.append(' : ' + comment);
            textBox.val(' ');
            div.appendTo($('#comments'));

        },
        error: function (msg) {
            console.dir(msg);
        }
    });
}