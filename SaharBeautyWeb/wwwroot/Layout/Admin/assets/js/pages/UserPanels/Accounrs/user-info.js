
$(document).on("click", ".edit-user-btn", function () {
    $.ajax({
        url: getUserInfoForEdit,
        type: 'Get',
        success: function (res, status, xhr) {
            const contentType = xhr.getResponseHeader("content-type") || "";
            if (contentType.includes("application/json")) {
                if (!res.success) {
                    handleApiError(res.error);
                    return;
                }
            }
            var modalEl = document.getElementById('staticBackdrop');
            // قرار دادن html پارشیال در body مودال
            $("#staticBackdrop .modal-body").html(res);
            // نمایش مودال
            var modal = new bootstrap.Modal(modalEl);

            modal.show();
            if ($.fn.persianDatepicker) {
                $("#staticBackdrop .Shamsi-date").persianDatepicker({
                    format: 'YYYY/MM/DD',
                    autoClose: true,
                    initialValue: false,
                    calendar: {
                        persian: {
                            locale: 'fa'
                        }
                    }
                });
            }
        },
        error: function (xhr) {
            let msg = (xhr.responseJSON?.error) || xhr.responseText || "خطایی پیش آمده";
            showPopup(msg)
        }
    });
})

$(document).on("change", "#avatarUpload", function () {
    var preview = $("#preview-admin-profile-image")[0];
    var inputLogo = $(this)[0];
    setUpImagePreview(inputLogo, preview);
});

$(document).on("click", ".apply-edit-profile", function (e) {
    e.preventDefault();
    var form = $(".edit-profile-form")[0];
    var formData = new FormData(form);

    $.ajax({
        url: applyEditProfile,
        processData: false,
        contentType: false,
        data: formData,
        type: 'Post',
        success: function (res) {
            if (res.success) {
                var modalEl = document.getElementById('staticBackdrop');
                var modal = bootstrap.Modal.getInstance(modalEl);
                modal.hide();
                form.reset();
                location.reload();
            } else {
                handleApiError(res.error);
                sendBtn.prop("disabled", false);
            }
        }, error: function () {
            showPopup("خطایی پیش آمده");
            var modal = new bootstrap.Modal(document.getElementById('staticBackdrop'));
            modal.hide();
            location.reload();
        }
    })
})