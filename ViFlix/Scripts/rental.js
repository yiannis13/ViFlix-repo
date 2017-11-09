$(document).ready(function() {

    $('#movie').on('keypress', function (e) {
        var code = e.keyCode || e.which;
        if (code === 13) {
            var movieName = $('#movie').val();
            $("#moviesUL").append('<li class="list-group-item">' + movieName + '</li>');
            e.preventDefault();
            $('#movie').val('');
        }
        
    });

});
