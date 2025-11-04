
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

$(document).on("change", "#clientAvatarUpload", function () {
    var preview = $("#clientAvatarPreview")[0];
    var inputAvatar = $(this)[0];
    setUpImagePreview(inputAvatar, preview);
});

$(document).on("click", "#apply-client-edit-profile", function (e) {
    e.preventDefault();
    var form = $("#edit-client-profile-form")[0];
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

$(document).on("click", "#apply-edit-client-profile-image", function (e) {
    e.preventDefault();
    var form = $("#edit-profile-image-client-form")[0];
    var image = $("#clientAvatarUpload")[0];
    var preview = $("#clientAvatarPreview")[0]
    var formData = new FormData(form);

    $.ajax({
        url: applyEditProfileImage,
        processData: false,
        contentType: false,
        data: formData,
        type: 'Post',
        success: function (res) {
            if (res.success) {
                $(preview).attr("src", "");
                const reader = new FileReader();
                reader.onload = function (e) {
                    $(preview).attr("src", e.target.result);
                };
                reader.readAsDataURL(image.files[0]);
            } else {
                handleApiError(res.error);
            }
        }, error: function (res) {
            handleApiError(res.error);
        }

    });
});