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
                appendComments(comment, commentsDiv);
            }
            commentsDiv.show();
            currentPage.val(+currentPage.val() + 1);
        },
        error: function (msg) {
            console.dir(msg);
        }
    });
    
})