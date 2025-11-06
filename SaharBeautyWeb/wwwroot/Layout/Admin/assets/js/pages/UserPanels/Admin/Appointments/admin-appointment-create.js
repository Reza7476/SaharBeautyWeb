$(document).on("change", "#treatment-select", function () {
    var selectedOPtion = $(this).find("option:selected");
    var serviceId = selectedOPtion.data("id");
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

            $("#input-treatmentId").val(serviceId); // ست اولیه (فقط id)
            $("#serviceDetails").html(res).removeClass("hidden");
        },
        error: function () {
            $(".time-slot-container").remove();
            $("#timeSlotContainer").empty();
        }
    });
});

$(document).on("click", ".day-card", function () {
    var dayCard = $(this);
    var day = dayCard.data("number");
    var duration = $("#treatment-duration").val();
    var date = dayCard.data("milady");
    $.ajax({
        url: getWeeklySchedule,
        data: { dayWeek: day, duration: duration, date: date },
        type: 'GET',
        success: function (res, status, xhr) {
            const contentType = xhr.getResponseHeader("content-type") || "";
            if (contentType.includes("application/json")) {
                if (!res.success) {
                    handleApiError(res.error);
                    return;
                }
            }

            var timeSlotSection = $('<div class="time-slot-container"></div>').html(res);


            if ($(window).width() <= 640) {
                dayCard.after(timeSlotSection);
            } else {
                $("#timeSlotContainer").html(timeSlotSection);
            }
        },
        error: function () {
            $(".time-slot-container").remove();
            $("#timeSlotContainer").empty();
        }
    });
});
