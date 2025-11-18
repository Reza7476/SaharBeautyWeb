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

    const form = $("#admin-appointment-filter-form");
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

});

$(document).on("change", "#status-dropdown", function () {
    var dropdown = $(this);
    var id = dropdown.data("id");
    var selectedValue = dropdown.val();
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        url: changeStatus,
        type: 'post',
        data: {
            id: id,
            __RequestVerificationToken: token,
            status: selectedValue
        },
        success: function (res) {
            if (res.success) {
                location.reload();
            } else {
                handleApiError(res.error);
            }
        },
        error: function (res) {
            handleApiError(res.error);
        }
    })
})

$(document).on("change", "#Day", function () {
    if ($(this).val()) {
        $("#Date").val('');
        $("#Date").prop("disabled", true);
    } else {
        $("#Date").prop("disabled", false);
    }
});

// 🔹 اگر تاریخ انتخاب شد → روز هفته بی‌اثر شود
$(document).on("change", "#Date", function () {
    if ($(this).val()) {
        $("#Day").val('');
        $("#Day").prop("disabled", true);
    } else {
        $("#Day").prop("disabled", false);
    }
});