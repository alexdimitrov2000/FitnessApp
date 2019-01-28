function appendComments(comment, commentsDiv) {
    let div = $('<div>').addClass('col-md-11 m-1');
    let image = '<img src=\"' + comment.profilePictureUrl + '\" width="45px" class="img-rounded"/>';
    var aTag = $('<a>').attr('href', `http://localhost:5000/users/profile/${comment.userName}`);
    aTag.text(comment.userName);
    div.append(image);
    div.append(aTag);
    div.append(' : ' + comment.content);
    let isAdmin = $('#isAdmin').val();
    let currentUser = $('#user').val();
    if (currentUser == comment.userName || isAdmin) {
        let dropdownTag = $('<a>').attr({
            'href': '#',
            'id': 'commentDropdown',
            'data-toggle': 'dropdown',
            'aria-haspopup': 'true',
            'aria-expanded': 'false',
        }).addClass('dropdown-toggle')
        let iTag = $('<i>').addClass('fas fa-ellipsis-h');
        dropdownTag.append(iTag);
        let deleteDiv = $('<div>').addClass('dropdown-menu dropdown-menu-left').attr('id', 'dropdown-menu-content');
        let deleteTag = $('<a>').addClass('deleteComment').attr({
            'href': comment.id,
            'id': 'commentOption'
        })
        let deleteItag = $('<i>').addClass('far fa-trash-alt');
        deleteTag.append(deleteItag);
        deleteTag.append(' Delete');
        deleteDiv.append(deleteTag);
        div.append(dropdownTag);
        div.append(deleteDiv);
    }
    commentsDiv.append(div);

    attachDeleteEvent();
}