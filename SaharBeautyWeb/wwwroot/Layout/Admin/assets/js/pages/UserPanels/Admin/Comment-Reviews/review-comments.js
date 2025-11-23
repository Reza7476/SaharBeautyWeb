$(document).on("change", ".publish-toggle", function () {
    var reviewId = $(this).data("id");
    var isPublished = $(this).is(":checked");
    var token = $('input[name="__RequestVerificationToken"]').val();
    ajaxWithButtonLoading({
        button: this,
        url: changePublished,
        type: 'Post',
        data: {
            id: reviewId,
            __RequestVerificationToken: token,
            publishStatus: isPublished
        },
        cuccess: function (res) {

        }
    });


})