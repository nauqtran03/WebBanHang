$(document).ready(function () {
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var total_quantity = $('#quantity_value').text();
        if (total_quantity != '') {
            quantity = parseInt(total_quantity)
        }
        alert(id + " " + quantity);
    });
});