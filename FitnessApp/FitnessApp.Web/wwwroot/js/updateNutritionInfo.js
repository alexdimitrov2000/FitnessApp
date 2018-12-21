let initialCalories;
let initialProtein;
let initialCarbohydrates;
let initialFats;
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
        dataType: 'json',
        success: function (response) {
            $('#foodName').text(response.name);
            $('#calories').text(response.calories);
            $('#protein').text(response.protein + 'g');
            $('#carbs').text(response.carbohydrates + 'g');
            $('#fats').text(response.fats + 'g');
            $('#multiplier').val(1);
            initialCalories = parseFloat(response.calories);
            initialProtein = parseFloat(response.protein);
            initialCarbohydrates = parseFloat(response.carbohydrates);
            initialFats = parseFloat(response.fats);
        }
    })
    
});

$('#multiplier').on('input', function changeValues() {
    let multiplier = parseFloat($('#multiplier').val());

    if (multiplier <= 0 || isNaN(multiplier) || !initialCalories) {
        $('#calories').text(0);
        $('#protein').text(0 + 'g');
        $('#carbs').text(0 + 'g');
        $('#fats').text(0 + 'g');
    }
    else {
        $('#calories').text(initialCalories * multiplier);
        $('#protein').text(initialProtein * multiplier + 'g');
        $('#carbs').text(initialCarbohydrates * multiplier + 'g');
        $('#fats').text(initialFats * multiplier + 'g');
    }
    
})