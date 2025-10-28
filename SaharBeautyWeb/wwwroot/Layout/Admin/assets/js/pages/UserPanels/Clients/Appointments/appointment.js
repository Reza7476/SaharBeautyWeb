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

                $("#input-treatmentId").val(serviceId);
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
        var date = dayCard.data("milady");
        // پاک کردن کلاس انتخاب شده قبلی
        $(".week-card").removeClass("selected").prop("disabled", false);

        // کارت انتخاب شده را غیرفعال و انتخاب شده کنیم
        dayCard.addClass("selected").prop("disabled", true);

        $.ajax({
            url: getWeeklySchedule,
            data: { dayWeek: day, duration: duration ,date:date},
            type: 'GET',
            success: function (res, status, xhr) {
                const contentType = xhr.getResponseHeader("content-type") || "";
                if (contentType.includes("application/json")) {
                    if (!res.success) {
                        handleApiError(res.error);
                        return;
                    }
                }

                $("#input-duration").val(duration);
                $("#input-date").val(date);
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

    $(document).on("click", ".time-slot-card", function () {

        var selectedTime = $(this).data("start");

        $("#input-time").val(selectedTime);

        $(".time-slot-card").removeClass("selected");
        $(this).addClass("selected");
    })

    $(document).on("click", "#reserve-btn", function (e) {
        e.preventDefault();
        var form = $("#reserveForm")[0];
        var formData = new FormData(form);
        $.ajax({
            url: reserveAppointment,
            type: "Post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (res) {
                if (res.success) {
                    location.reload();
                }
                else if (res.statusCode == 400) {
                    Object.keys(res.error).forEach(function (key) {
                        const fieldName = key.replace("AppointmentModel.", "");
                        const messages = res.error[key];
                        const span = $(`span[data-valmsg-for='AppointmentModel.${fieldName}']`);
                        span.text(messages.join(", "));
                        span.css("color", "red");
                    });
                } else if (res.statusCode != 200 || res.statusCode != 400) {
                    handleApiError(res.error);
                }
            },
            error: function (res) {

            }
        });
    })


});
