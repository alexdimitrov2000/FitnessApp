$('#moreComments').click(function loadMoreComments() {
    let commentsDiv = $('#comments');
    let url = 'http://localhost:5000/api/comments/loadcomments';
    let pageSize = $('#pageSize').val();
    let currentPage = $('#currentPage');
    let postId = $('#currentId').val();
    let data = {
        'pageSize': pageSize,
        'currentPage': currentPage.val(),
        'postId': postId
    };
    $.ajax({
        url: url,
        method: "GET",
        data: data,
        dataType: 'json',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        success: function (response) {
            for (let comment of response) {
                appendComments(comment);
            }
            commentsDiv.show();
            currentPage.val(+currentPage.val() + 1);
            moreComments.show();
        },
        error: function (msg) {
            console.dir(msg);
        }
    });

    function appendComments(comment) {
        let div = $('<div>').addClass('col-md-11 m-1');
        let image = '<img src=\"' + comment.profilePictureUrl + '\" width="45px" class="img-rounded"/>';
        var aTag = $('<a>').attr('href', `http://localhost:5000/users/profile/${comment.userName}`);
        aTag.text(comment.userName);
        div.append(image);
        div.append(aTag);
        div.append(' : ' + comment.content);
        commentsDiv.append(div);
    }
})