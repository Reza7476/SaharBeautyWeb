
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

    ajaxWithButtonLoading({
        button: this,
        url: cancelAppointment,
        data: {
            id: id,
            __RequestVerificationToken: token
        },
        type: 'Post',
        success: function (res)
        {
            location.reload();
        }
    });

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


    ajaxWithButtonLoading({
        button: this,
        url: addCommentPartial,
        type: 'Get',
        data: { appointmentId: id },
        success: function (html)
        {
            var modal = new bootstrap.Modal(document.getElementById('staticBackdrop'));
            $("#staticBackdrop .modal-body").html(html);
            modal.show();
        }
    });
})

$(document).on("click", "#add-client-comment-btn", function (e) {
    e.preventDefault();
    var rate = $("input[name='rate']:checked").val();
    $("#selected-rate").val(rate);

    var form = $("#reviewForm")[0];
    var formData = new FormData(form);

    ajaxWithButtonLoading({
        button: "#add-client-comment-btn",
        url: addClientComment,
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (res) {
            location.reload();
        }
    })
})