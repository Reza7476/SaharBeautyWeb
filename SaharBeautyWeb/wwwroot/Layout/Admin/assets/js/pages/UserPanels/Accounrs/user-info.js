
$(document).on("click", ".edit-user-btn", function () {

    ajaxWithButtonLoading({
        button: this,
        url: getUserInfoForEdit,
        type: 'Get',
        success: function (res, status, xhr)
        {
            var modalEl = document.getElementById('staticBackdrop');

            $("#staticBackdrop .modal-body").html(res);
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
        }
    });
})

$(document).on("change", "#avatarUpload", function () {
    var preview = $("#avatarPreview")[0];
    var inputAvatar = $(this)[0];
    setUpImagePreview(inputAvatar, preview);
});

$(document).on("click", "#apply-admin-edit-profile", function (e) {
    e.preventDefault();
    var form = $("#edit-admin-profile-form")[0];
    var formData = new FormData(form);

    ajaxWithButtonLoading({
        button: "#apply-admin-edit-profile",
        url: applyEditProfile,
        processData: false,
        contentType: false,
        data: formData,
        type: 'Post',
        success: function (res)
        {
            var modalEl = document.getElementById('staticBackdrop');
            var modal = bootstrap.Modal.getInstance(modalEl);
            modal.hide();
        }
    });
})

$(document).on("click", "#apply-edit-admin-profile-image", function (e) {
    e.preventDefault();
    var form = $("#edit-profile-image-form")[0];
    var image = $("#avatarUpload")[0];
    var preview = $("#avatarPreview")[0]
    var formData = new FormData(form);

    ajaxWithButtonLoading({
        button: "#apply-edit-admin-profile-image",
        url: applyEditProfileImage,
        processData: false,
        contentType: false,
        data: formData,
        type: 'Post',
        success: function (res)
        {
            $(preview).attr("src", "");
            const reader = new FileReader();
            reader.onload = function (e) {
                $(preview).attr("src", e.target.result);
            };
            reader.readAsDataURL(image.files[0]);
        }
    })
});