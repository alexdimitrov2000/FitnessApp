function attachDeleteEvent() {
    $('.deleteComment').click(function remove() {
        event.preventDefault();

        let baseUrl = 'http://localhost:5000/api/comments';
        let clicked = $(this);
        let id = clicked.attr('href');

        $.ajax({
            url: baseUrl + `/RemoveComment/${id}`,
            method: "POST",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            success: function () {
                clicked.parent().parent().remove();
            },
            error: function (msg) {
                console.dir(msg);
            }
        });
    })
}