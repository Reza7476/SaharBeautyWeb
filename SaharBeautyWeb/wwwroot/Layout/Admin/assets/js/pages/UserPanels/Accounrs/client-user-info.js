
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

$(document).on("change", "#clientAvatarUpload", function () {
    var preview = $("#clientAvatarPreview")[0];
    var inputAvatar = $(this)[0];
    setUpImagePreview(inputAvatar, preview);
});

$(document).on("click", "#apply-client-edit-profile", function (e) {
    e.preventDefault();
    var form = $("#edit-client-profile-form")[0];
    var formData = new FormData(form);

    ajaxWithButtonLoading({
        button: "#apply-client-edit-profile",
        url: applyEditProfile,
        processData: false,
        contentType: false,
        data: formData,
        type: 'Post',
        success: function (res) {

            var modalEl = document.getElementById('staticBackdrop');
            var modal = bootstrap.Modal.getInstance(modalEl);
            modal.hide();
            location.reload();
        }
    });


})

$(document).on("click", "#apply-edit-client-profile-image", function (e) {
    e.preventDefault();
    var form = $("#edit-profile-image-client-form")[0];
    var image = $("#clientAvatarUpload")[0];
    var preview = $("#clientAvatarPreview")[0]
    var formData = new FormData(form);

    ajaxWithButtonLoading({
        button: "#apply-edit-client-profile-image",
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

    });


});