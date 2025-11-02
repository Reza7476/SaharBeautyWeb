
$(document).ready(function () {
    if ($.fn.persianDatepicker) {
        $("#Date").persianDatepicker({
            format: 'YYYY/MM/DD',
            autoClose: true,
            initialValue: false,
            observer: true, // اضافه کردن این گزینه برای re-render
            calendar: {
                persian: {
                    locale: 'fa'
                }
            }
        });
    } 
});


$(document).on("click", "#remove-filter", function () {
    var form = $("#filter-form")[0];
    form.reset();
})

$(document).on("click", ".cancel-appointment-btn", function () {
    var id = $(this).data("id");
    var token = $('input[name="__RequestVerificationToken"]').val();

    $.ajax({
        url: cancelAppointment,
        data: {
            id: id,
            __RequestVerificationToken: token
        },
        type:'Post',
        success: function (res) {
            if (res.success) {
                location.reload();
            } else {
                HandlerApiError(res.error);
            }
        }
    })


})