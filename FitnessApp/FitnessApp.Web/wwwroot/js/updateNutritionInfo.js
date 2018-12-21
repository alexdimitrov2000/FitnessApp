$('.update-item').click(function update() {
    event.preventDefault();
    let baseUrl = "http://localhost:5000"

    $.ajax({
        url: baseUrl + $(this).attr('href'),
        method: "GET",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        async: false,
        dataType: 'json',
        success: function (response) {
            $('#calories').text(response.calories);
            $('#protein').text(response.protein + 'g');
            $('#carbs').text(response.carbohydrates + 'g');
            $('#fats').text(response.fats + 'g');
        }
    })
    
});