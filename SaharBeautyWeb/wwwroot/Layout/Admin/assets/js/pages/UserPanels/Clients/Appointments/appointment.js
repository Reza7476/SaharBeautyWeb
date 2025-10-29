$(document).ready(function () {

    $("#reserve-btn").prop("disabled", true);

    $(document).on("change", "#serviceSelect", function () {
        var selectedOption = $(this).find("option:selected");
        var serviceId = selectedOption.data("id");

        $("#input-treatmentId").val('');
        $("#input-duration").val('');
        $("#input-date").val('');
        $("#input-time").val('');
        $("#input-day-week").val('');
        $("#serviceDetails").addClass("hidden").html('');
        $(".time-slot-container").remove();         // remove rendered slots
        $("#timeSlotContainer").empty();            // ensure container empty
        $(".week-card").removeClass("selected").prop("disabled", false);
        $("#reserve-btn").prop("disabled", true);  // disable reserve until valid

        if (!serviceId) return;

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

    $(document).on("click", ".week-card", function () {
        var dayCard = $(this);
        var day = dayCard.data("number");
        var duration = $("#treatment-duration").val(); 
        var date = dayCard.data("milady");
        var treatmentId = $("#input-treatmentId").val();

        if (!treatmentId) {
            return;
        }

        if (!duration) {
            return;
        }
        
        $(".week-card").removeClass("selected").prop("disabled", false);
        $(".time-slot-container").remove();
        $("#timeSlotContainer").empty();
        $("#input-duration").val(duration);
        $("#input-date").val(date);
        $("#input-day-week").val(day);
        $("#input-time").val('');
        $("#reserve-btn").prop("disabled", true);

        dayCard.addClass("selected").prop("disabled", true);

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

    $(document).on("click", ".time-slot-card", function () {
        var treatmentId = $("#input-treatmentId").val();
        var date = $("#input-date").val();
        var duration = $("#input-duration").val();

        if (!treatmentId || !date || !duration) {
            return;
        }

        var slotTreatmentId = $(this).data("treatment-id");
        if (slotTreatmentId && String(slotTreatmentId) !== String(treatmentId)) {
            return;
        }

        var selectedTime = $(this).data("start");
        $("#input-time").val(selectedTime);

        $(".time-slot-card").removeClass("selected");
        $(this).addClass("selected");

        $("#reserve-btn").prop("disabled", false);
    });

    $(document).on("click", "#reserve-btn", function (e) {
        e.preventDefault();

        var treatmentId = $("#input-treatmentId").val();
        var date = $("#input-date").val();
        var time = $("#input-time").val();
        var duration = $("#input-duration").val();

        if (!treatmentId || !date || !time || !duration) {
            return;
        }

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
                } else {
                    handleApiError(res.error);
                }
            },
            error: function (res) {
            }
        });
    });

});
