

$(document).on("click", ".save-day-schedule", function () {
    const raw = $(this).closest("tr");
    const startTime = raw.find(".start-time").val();
    const endTime = raw.find(".end-time").val();
    const isActive = raw.find(".isActive").is(":checked");
    const dayOfWeek = raw.data("day");
    const id = raw.data("id");
   
    var token = $('input[name="__RequestVerificationToken"]').val();
    const formData = new FormData();
    formData.append("Id", id);
    formData.append("DayOfWeek", dayOfWeek);
    formData.append("StartTime", startTime);
    formData.append("EndTime", endTime);
    formData.append("IsActive", isActive);
    formData.append("__RequestVerificationToken", token);

    $.ajax({
        url: saveNewSchedule,
        type: 'Post',
        data: formData,
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.success) {
                location.reload();
            } else {

                handleApiError(res.error);
            }
        }, error: function (res) {
            handleApiError(res.error);
        }
    })

})
