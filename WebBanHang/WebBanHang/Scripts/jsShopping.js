$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var total_quantity = $('#quantity_value').text();
        if (total_quantity != '') {
            quantity = parseInt(total_quantity)
        }
        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.success) {
                    $('#checkout_items').html(rs.count);
                    alert(rs.msg);
                }
            }
        });
    });
    $('body').off('click', '.btnDeleteAll').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        var conf = confirm("Do you want to remove all item from your cart?");
        if (conf == true) {
            DeleteAll();
        }
    });
    $('body').off('click', '.btnDelete').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm("Do you want to remove this item from your cart?");
        if (conf == true) {
            $.ajax({
                url: '/shoppingcart/delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.success) {
                        $('#checkout_items').html(rs.count);
                        $('#trow_' + id).remove();
                        
                        if ($('#cartBody tr').length === 0) {
                            $('#noItemRow').show();
                        }
                        LoadCart();
                    }
                }
            });
        } 
    });
    $('body').on('click', '.btnIncrease', function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        var input = $("#Quantity_" + id);
        var qty = parseInt(input.val());
        Update(id, qty + 1)
    });
    $('body').on('click', '.btnDecrease', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var input = $('#Quantity_' + id);
        var qty = parseInt(input.val());
        if (qty > 1) {
            Update(id, qty - 1);
        }
    });
});

function ShowCount() {
    $.ajax({
        url: '/shoppingcart/showcount',
        type: 'GET',
        success: function (rs) {
            $('#checkout_items').html(rs.count);   
        }
    });
}
function Update(id,quantity) {
    $.ajax({
        url: '/shoppingcart/update',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs.success) {
                LoadCart(); 
            }
        }
    });
}
function LoadCart() {
    $.ajax({
        url: '/shoppingcart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
        }
    });
}
function DeleteAll() {
    $.ajax({
        url: '/shoppingcart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.success) {
                LoadCart();
            } else {
                alert(rs.msg)
            }
        }
    });
}
