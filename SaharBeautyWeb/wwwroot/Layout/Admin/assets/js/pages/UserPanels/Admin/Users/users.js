
$("#user-search").on("input", function () {

    const value = $(this).val().replace(/\D/g, "");
    $(this).val(value);
    if (value.length === 11) {
        $("#search-form")[0].submit();
    } else if (value.length === 0) {
        $("#search-form")[0].submit();
    }
});

$(document).on("change", ".toggle-switch", function () {
    const userCard = $(this).closest(".user-card");
    const userId = userCard.data("user-id");
    const active = $(this).is(":checked");
    var token = $('input[name="__RequestVerificationToken"]').val();

    $.ajax({
        url: changeUserActivate,
        type: 'Post',
        data: {
            userId: userId,
            __RequestVerificationToken: token,
            activate: active
        },
        success: function (res) {
            if (res.success) {

            } else {
                handleApiError(res.error);
            }
        }, error: function (res) {
            handleApiError(res.error);
        }
    });
});