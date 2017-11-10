$(document).ready(function () {

    var viewModel = {
        "movieNames": []
    };

    // on enter keystroke event
    $('#movie').on('keypress', function (event) {
        var code = event.keyCode || event.which;
        var movie = $('#movie').val();
        if (code === 13 && movie.length !==0) { 
            $("#moviesUL").append('<li class="list-group-item">' + movie + '</li>');
            event.preventDefault();
            $('#movie').val('');
            viewModel.movieNames.push(movie);
        }
    });

    // on button click event
    $("#rentalform").on("click",".btn",
        function () {
            viewModel.customerId = $('#customerId').val();

            if ($('#movie').val().length !== 0) { // include also the value that is currently in the input text if is not empty
                viewModel.movieNames.push($('#movie').val());
            }

            $.ajax({
                url: "/api/rental",
                type: 'POST',
                data: viewModel,
                success: function () {
                    toastr.success("Rental succeeded. Enjoy your movie :)");
                },
                error: function () {
                    alert("what the fuck");

                    toastr.error("Sorry. Something unexpected happened :(");
                    
                }
            });
        });
});
