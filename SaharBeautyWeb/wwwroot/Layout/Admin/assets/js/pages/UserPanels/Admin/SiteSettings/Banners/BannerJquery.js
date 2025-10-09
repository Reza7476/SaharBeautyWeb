$(() => {

    $(document).on("change", "#add-banner-image", function () {
        var input = this;
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#add-new-image-banner").attr("src", e.target.result);
            }
        }
        reader.readAsDataURL(input.files[0]);
    })

    $("#createBanner").on("click", function (e) {
        e.preventDefault();
        var btnSend = $(this);

        var title = $("#add-banner-title").val();
        var imageFile = $("#add-banner-image")[0].files[0];
        if (!title || !imageFile) {
            showPopup("عنوان و عکس نباید خالی باشند ")
            return;
        }

        var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
        if (!allowedExtensions.exec(imageFile.name)) {
            showPopup("فرمت تصویر باید jpg، jpeg یا png باشد.")
            return;
        }
        var form = $("#add-banner-form")[0]
        let formData = new FormData(form);

        btnSend.prop("disabled", true);
        $.ajax({
            url: createBanner,
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.success) {
                    location.reload();
                } else {
                    handleApiError(res.error);
                    btnSend.prop("disabled", false);
                    return
                }
            },
            error: function (err) {
                handleApiError(err);
                btnSend.prop("disabled", false);
                return;
            }
        });
    });

    $(".edit-banner-btn").on("click", function () {
        var bannerId = $(this).data("id");
        var btnSend = $(this);
        $(this).prop("disabled", true);
        $.ajax({
            url: editBannerPartial,
            type: 'GET',
            data: { id: bannerId },
            success: function (res, status, xhr) {
                const contentType = xhr.getResponseHeader("content-type") || "";
                if (contentType.includes("application/json")) {
                    if (!res.success) {
                        handleApiError(res.error);
                        btnSend.prop("disabled", false);
                        return;
                    }
                }
                $("#staticBackdrop .modal-body").html(res);
                var modal = new bootstrap.Modal(document.getElementById('staticBackdrop'));
                modal.show();
            },
            error: function () {
                showPopup("خطا در بارگذاری فرم ویرایش!");
                btnSend.prop("disabled", false);
            },
            complete: function () {

                btnSend.prop("disabled", false);
            }
        });
    })

    $(document).on("change", "#input-image-banner-update", function () {
        var input = this;
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $("#new-banner-image").attr("src", e.target.result);
            }
        }
        reader.readAsDataURL(input.files[0]);
    })

    $(document).on("click", "#saveBannerBtn", function () {

        var title = $("#editBannerTitle").val();
        var imageInput = $("#input-image-banner-update")[0];

        if (!title || (!imageInput.files || imageInput.files.length === 0)) {
            showPopup("عنوان یا عکس نباید خالی باشند");
            return;
        }

        var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
        if (!allowedExtensions.exec(imageInput.name)) {
            showPopup("فرمت تصویر باید jpg، jpeg یا png باشد.")
            return;
        }

        var form = $("#editBannerForm")[0];
        var formData = new FormData(form);

        $.ajax({
            url: '@Url.Page("Index", "EditBanner")',
            type: 'Post',
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: function () {
                $("#saveBannerBtn").prop("disabled", true);
            },
            success: function (res) {
                if (res.success) {
                    var modalEl = document.getElementById('staticBackdrop');
                    var modal = bootstrap.Modal.getInstance(modalEl);
                    modal.hide();
                    $("#editBannerForm")[0].reset();
                    location.reload();
                } else {
                    handleApiError(res.error || "خطا در ویرایش");
                }
            },
            error: function () {
                showPopup("خطایی رخ داده");
                var modal = new bootstrap.Modal(document.getElementById('staticBackdrop'));
                modal.hide();
            },
            complete: function () {
                $("#saveBannerBtn").prop("disabled", false);
            }
        });
    });

});
