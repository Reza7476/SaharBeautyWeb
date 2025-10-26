$(document).on("change", "#serviceSelect", function () {
    var selectedOption = $(this).find("option:selected");
    var serviceId = selectedOption.data("id");

    $.ajax({
        url: getTreatmentDetails,
        data: {id:serviceId},
        type:'Get',
        success: function (res, status, xhr) {
            const contentType = xhr.getResponseHeader("content-type") || "";
            if (contentType.includes("application/json")) {
                if (!res.success) {
                    handleApiError(res.error);
                    btnSend.prop("disabled", false);
                    return;
                }
            }
            $("#serviceDetails ").html(res);
        },
    });
})



