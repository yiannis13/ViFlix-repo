$(document).ready(function () {
    $("#customers").on("click", ".js-del",
        function () {
            var button = $(this);

            bootbox.confirm("Are you sure you want to delete this customer?", function(confirmed) {
                if (confirmed) {
                    $.ajax({
                        url: "/api/customers/" + button.attr("customer-id"),
                        method: "DELETE",
                        success: function () {
                            button.parents("tr").remove();
                        }
                    });
                }
            });
        });
});