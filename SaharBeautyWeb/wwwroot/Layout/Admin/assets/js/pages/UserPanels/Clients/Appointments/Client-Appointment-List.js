
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


$(document).on("click", "#remove-filter", function (e) {
    e.preventDefault();

    const form = $("#filter-form");
    form.trigger("reset");

    form.find("select").each(function () {
        $(this).prop("selectedIndex", 0);
        $(this).prop("disabled", false);
    })

    form.find("input[type='text']").each(function () {
        $(this).val("");                  // خالی شود
        $(this).prop("disabled", false);  // فعال شود
    });
    


    fetch(window.location.pathname)
        .then(response => response.text())
        .then(html => {
            const newContent = $(html).find(".appointments-card").html();
            $(".appointments-card").html(newContent);
        })
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

$(document).on("change", "#Day", function () {
    if ($(this).val()) {
        $("#Date").val('');                // مقدار تاریخ خالی شود
        $("#Date").prop("disabled", true); // غیرفعال شود
    } else {
        $("#Date").prop("disabled", false); // اگر پاک شد دوباره فعال شود
    }
});

// اگر تاریخ انتخاب شد → روز هفته غیرفعال شود
$(document).on("change", "#Date", function () {
    if ($(this).val()) {
        $("#Day").val('');                 // مقدار روز هفته خالی شود
        $("#Day").prop("disabled", true);  // غیرفعال شود
    } else {
        $("#Day").prop("disabled", false); // اگر پاک شد دوباره فعال شود
    }
});

$(document).on("click", ".add-appointment-review-btn", function () {
    var btn = $(this);
    var id = btn.data("id");
    alert(id);
})