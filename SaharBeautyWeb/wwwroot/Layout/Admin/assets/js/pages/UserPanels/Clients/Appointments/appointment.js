//$(() => {
//    var dayBtn = $(".week-card").prop("disabled", true);
//})
//$(document).on("change", "#serviceSelect", function () {
//    var selectedOption = $(this).find("option:selected");
//    var serviceId = selectedOption.data("id");

//    $.ajax({
//        url: getTreatmentDetails,
//        data: { id: serviceId },
//        type: 'Get',
//        success: function (res, status, xhr) {
//            const contentType = xhr.getResponseHeader("content-type") || "";
//            if (contentType.includes("application/json")) {
//                if (!res.success) {
//                    handleApiError(res.error);
//                    btnSend.prop("disabled", false);
//                    return;
//                }
//            }
//            $("#serviceDetails ").html(res);
//            var dayBtn = $(".week-card").prop("disabled", false);
//        },
//    });
//})



//$(document).on("click", ".week-card", function () {
//    var day = $(this).data("number");
//    var duration = $("#treatment-duration").val();

//    $.ajax({
//        url: getWeeklySchedule,
//        data: {
//            dayWeek: day,
//            duration: duration
//        },
//        type: 'Get',
//        success: function (res, status, xhr) {
//            const contentType = xhr.getResponseHeader("content-type") || "";
//            if (contentType.includes("application/json")) {
//                if (!res.success) {
//                    handleApiError(res.error);
//                    return;
//                }
//            }
//            $("#timeSlotContainer ").html(res);
//        },
//    });
//})





$(document).ready(function () {

    // وقتی یک خدمت انتخاب شد
    $(document).on("change", "#serviceSelect", function () {
        var selectedOption = $(this).find("option:selected");
        var serviceId = selectedOption.data("id");

        $.ajax({
            url: getTreatmentDetails,
            data: { id: serviceId },
            type: 'GET',
            success: function (res, status, xhr) {
                const contentType = xhr.getResponseHeader("content-type") || "";
                if (contentType.includes("application/json")) {
                    if (!res.success) {
                        handleApiError(res.error);
                        return;
                    }
                }
                $("#serviceDetails").html(res).removeClass("hidden");
                $(".week-card").removeClass("selected").prop("disabled", false);
            }
        });
    });

    // وقتی روی یک روز هفته کلیک شد
    $(document).on("click", ".week-card", function () {
        var dayCard = $(this);
        var day = dayCard.data("number");
        var duration = $("#treatment-duration").val();

        // پاک کردن کلاس انتخاب شده قبلی
        $(".week-card").removeClass("selected").prop("disabled", false);

        // کارت انتخاب شده را غیرفعال و انتخاب شده کنیم
        dayCard.addClass("selected").prop("disabled", true);

        $.ajax({
            url: getWeeklySchedule,
            data: { dayWeek: day, duration: duration },
            type: 'GET',
            success: function (res, status, xhr) {
                const contentType = xhr.getResponseHeader("content-type") || "";
                if (contentType.includes("application/json")) {
                    if (!res.success) {
                        handleApiError(res.error);
                        return;
                    }
                }

                // حذف محتوای قبلی
                $(".time-slot-container").remove();

                // ایجاد یک div جدید برای اسلات‌ها
                var timeSlotSection = $('<div class="time-slot-container"></div>').html(res);

                // موبایل: اسلات‌ها زیر کارت انتخاب شده
                if ($(window).width() <= 640) {
                    dayCard.after(timeSlotSection);
                } else {
                    $("#timeSlotContainer").html(timeSlotSection);
                }
            }
        });
    });

    // تغییر اندازه صفحه (ریسپانسیو)
    $(window).resize(function () {
        var selectedDay = $(".week-card.selected");
        var timeSlotContainer = $(".time-slot-container");

        if (selectedDay.length && timeSlotContainer.length) {
            if ($(window).width() <= 640) {
                selectedDay.after(timeSlotContainer);
            } else {
                $("#timeSlotContainer").html(timeSlotContainer);
            }
        }
    });
});
