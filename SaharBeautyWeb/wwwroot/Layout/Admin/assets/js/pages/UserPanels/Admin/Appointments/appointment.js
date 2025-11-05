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

    // تمام فیلدهای فرم را ریست کن
    const form = $("#admin-appointment-filter-form");
    form.trigger("reset");

    // ورودی‌های select را به گزینه‌ی اول (همه) برگردان
    form.find("select").prop("selectedIndex", 0);

    // فیلدهای text و hidden را هم خالی کن
    form.find("input[type='text'], input[type='hidden']").val("");

    // حالا با AJAX محتوای جدول را بدون فیلتر دوباره بگیر
    fetch(window.location.pathname)
        .then(response => response.text())
        .then(html => {
            const newContent = $(html).find(".appointments-card").html();
            $(".appointments-card").html(newContent);
        })

});