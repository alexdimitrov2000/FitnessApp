$('#viewComments').click(function showComments() {
    let commentsDiv = $('#comments');
    let isFirstAjax = $('#isFirstAjax');
    let moreComments = $('#moreComments');
    if (commentsDiv.css('display') == 'none') {
        if (isFirstAjax.val() == 'true') {
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
                    moreComments.show();
                    isFirstAjax.val(false);
                },
                error: function (msg) {
                    console.dir(msg);
                }
            });
        }
        else {
            commentsDiv.show();
            moreComments.show();
        }
    }
    else {
        if (commentsDiv.css('display') != 'none') {
            commentsDiv.hide();
            moreComments.hide();
        }
    }
       
})