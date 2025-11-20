
$(document).on("click", ".client-card", function () {
    var selectedClient = $(this);
    var clientId = selectedClient.data("id");
    clearInputs();
    $(".time-slot-card.active").removeClass("selected");
    $(".day-card").removeClass("selected");
    $(".client-card").removeClass("selected");
    selectedClient.addClass("selected");

    var inputClient = $("#input-client-id").val(clientId);
})
$(document).on("change", "#treatment-select", function () {
    clearInputs();
    var selectedOPtion = $(this).find("option:selected");
    var serviceId = selectedOPtion.data("id");

    $(".time-slot-card.active").removeClass("selected");
    $(".day-card").removeClass("selected");

    ajaxWithButtonLoading({

        button: "#treatment-select",
        url: getTreatmentDetails,
        data: { id: serviceId },
        type: 'GET',
        success: function (res, status, xhr)
        {
            $("#input-treatmentId").val(serviceId); // ست اولیه (فقط id)
            $("#serviceDetails").html(res).removeClass("hidden");
        }
    });
});

$(document).on("click", ".day-card", function () {

    var selectedDay = $(this);
    var dayOfWeek = selectedDay.data("number");
    var duration = $("#treatment-duration").val();
    var date = selectedDay.data("milady");
    $(".time-slot-card.active").removeClass("selected");
    $(".day-card").removeClass("selected");
    selectedDay.addClass("selected");


    var inputDuration = $("#input-duration");
    var inputDate = $("#input-date").val("");
    var inputDayWeek = $("#input-day-week").val("");
    var inputTime = $("#input-time").val("");

    ajaxWithButtonLoading({
        button: this,
        url: getWeeklySchedule,
        data: { dayWeek: dayOfWeek, date: date },
        type: 'GET',
        success: function (res, status, xhr)
        {
            var timeSlotSection = $('<div class="time-slot-container"></div>').html(res);
            inputDate.val(date);
            inputDayWeek.val(dayOfWeek);
            if ($(window).width() <= 640) {
                selectedDay.after(timeSlotSection);
            } else {
                $("#timeSlotContainer").html(timeSlotSection);
            }
        }
    })
});

$(document).on("click", ".time-slot-card", function () {
    var selectedCard = $(this);
    if (selectedCard.hasClass("inactive")) {

        return;
    }
    var inputTime = $("#input-time").val("");
    $(".time-slot-card.active").removeClass("selected");
    selectedCard.addClass("selected");

    var time = selectedCard.data("start");
    inputTime.val(time);
})

$(document).on("click", "#create-admin-appointment-btn", function (e) {
    e.preventDefault();
    var form = $("#admin-reserve-form")[0];
    var formData = new FormData(form);


    ajaxWithButtonLoading({
        button: "#create-admin-appointment-btn",
        url: reserveAdminAppointment,
        data: formData,
        type: "Post",
        contentType: false,
        processData: false,
        success: function (res)
        {
            location.reload();
        }
    })
})



function clearInputs() {
    var inputTreat = $("#input-treatmentId").val("");
    var inputDate = $("#input-date").val("");
    var inputTime = $("#input-time").val("");
    var inputDayWeek = $("#input-day-week").val("");
}



$("#client-search").on("input", function () {
    var search = $(this).val().trim().toLowerCase();  
    $(".client-card").each(function () { 
        var phone = $(this).find('.client-phone').text().toLowerCase(); 

        if (phone.includes(search)) { 
            $(this).show();
        } else {
            $(this).hide();
        }
    });
});