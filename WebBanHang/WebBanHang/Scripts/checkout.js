$(document).ready(function () {
    function OnBeginCO() {
        $('#btnCheckOut').attr("disabled", "disabled");
        $('#load_send').html("<h2>Sending......</h2>");
    }

    function OnSuccessCO(res) {
        $('#btnCheckOut').removeAttr("disabled");
        $('#load_send').html("");
        if (!res || !res.responseJSON) {
            alert("Lỗi phản hồi từ server!");
            return;
        }
        var data = res.responseJSON;
        if (data && data.success) {
            if (data.code == 1 || data.code == 2) {
                location.href = data.url; // Chuyển hướng đến CheckOutSuccess
            }
        } else {
            alert(data.msg || "Đặt hàng thất bại, vui lòng kiểm tra lại thông tin!");
        }
    }

    function OnFailure(rs) {
        $('#btnCheckOut').removeAttr("disabled");
        $('#load_send').html("");
        if (!rs.success) {
            $('#show_fail').html("Failed to place order. Please check your information and try again.");
        } else {
            alert("Có lỗi xảy ra khi gửi dữ liệu!");
        }
    }

    $('body').on('change', '#drTypeMethod', function () {
        var type = $(this).val();
        $('#load_form_payment').hide();
        if (type == "2") {
            $('#load_form_payment').show();
        } else {
            $('#load_form_payment').hide();
        }
    });

    $('#MyForm').validate({
        rules: {
            'CustomerName': { required: true },
            'Phone': { required: true },
            'Address': { required: true },
            'Email': { required: true, email: true }
        },
        messages: {
            'CustomerName': 'Name is required',
            'Phone': 'Phone is required',
            'Address': 'Address is required',
            'Email': 'Please enter a valid email address'
        }
    });
});