$(() => {

    $(document).on("change", ".whyus-section-image-input", function () {
        var inputImage = $(".whyus-section-image-input")[0];
        var previewImage = $(".whyus-section-preview-img")[0];
    setUpImagePreview(inputImage, previewImage);
    })

    $(document).on("click", "#createWhyUsSection", function (e) {
        e.preventDefault();
        var send = $(this);
        var form = $("#add-whyus-section-form")[0];
        var formData = new FormData(form);
                send.prop("disabled", true);
        $.ajax({
            url: createWhyUsSectionUrl,
            type: 'Post',
            data: formData,
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.success) {
                    location.reload();
                } else {
                    handleApiError(res.error);
                    send.prop("disabled", false);
                    return;
                }
            },
            error: function (err) {
                handleApiError(err);
                send.prop("disabled", false);
            }
        });
    })

    $(document).on("click", ".add-why-us-question-btn", function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        $.ajax({
            url: addWhyUsQuestionPartialUrl,
            type: 'Get',
            data: { id: id },
            success: function (html) {
                $("#staticBackdrop .modal-body").html(html);
                var modal = new bootstrap.Modal(document.getElementById('staticBackdrop'));
                modal.show();
            },
            error: function () {
                showPopup("خطایی پیش امده")
            }

        })
    });

    $(document).on("click", "#add-why-us-question", function (e) {
        e.preventDefault();
        var sendBtn = $(this);
        sendBtn.prop("disabled", true);
        var form = $("#add-why-us-question-form")[0];
        var formData = new FormData(form);
        $.ajax({
            url: addQuestionsUrl,
            type: 'Post',
            data: formData,
            processData: false,
            contentType: false,
            beforeSend: function () {
                sendBtn.prop("disabled", true);
            },
            success: function (res) {
                if (res.success) {
                    location.reload();
                } else {
                    handleApiError(res.error);
                    sendBtn.prop("disabled", false);
                }
            },
            error: function () {
                showPopup("خطایی پیش امده")
                location.reload();
            }
        });
    })

    $(document).on("click", ".delete-question-btn", function (e) {
        e.preventDefault();
        var removeBtn = $(this);
        var id = removeBtn.data("id");
        removeBtn.prop("disabled", true);
        var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            url: deleteQuestionUrl,
            type: 'Post',
            data: {
                id: id,
                __RequestVerificationToken: token
            },
           
            success: function (res) {
                if (res.success) {
                    location.reload();
                } else {
                    handleApiError(res.error);
                    removeBtn.prop("disabled", false);
                }
            }, error: function () {
                showPopup("خطای پیش آمده");
                removeBtn.prop("disabled", false);
            }
        });
    });

    $(document).on("click", ".edit-why-us-section-btn", function (e) {
        e.preventDefault();
        var btnSend = $(this);
        btnSend.prop("disabled", true);
        id = $(this).data("id");
        $.ajax({
            url: editWhyUsSectionEditUrl,
            type: 'Get',
            data: { id: id },
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
            error: function (xhr, status, error) {
                showPopup("خطا در بارگذاری فرم ویرایش!");
                btnSend.prop("disabled", false);
            }

        });
    });

    $(document).on("click", ".edit-why-us-section-title-description", function (e) {
        e.preventDefault();
        var sendBtn = $(this);
        sendBtn.prop("disabled", true);
        var form = $("#edit-why-us-section-title-description-form")[0];
        var formData = new FormData(form);

        $.ajax({
            url: applyEditTitleAndDescripWhyUsSection,
            type: 'Post',
            processData: false,
            contentType: false,
            data: formData,
            success: function (res) {
                if (res.success) {
                    location.reload();
                } else {
                    handleApiError(res.error);
                    location.reload();
                    sendBtn.prop("disabled", false);
                }
            },
            error: function () {
               
                handleApiError("خطایی در بارگذاری فرم ویزرایش پیش امده");
                location.reload();
                sendBtn.prop("disabled", false);
            }
        });

    });

    $(document).on("click", "#show-edit-section-form-div", function (event) {
        event.preventDefault();
        $("#edit-why-us-section-image-div").css("display", "block");
    });

    $(document).on("click", ".apply-edited-why-us-image", function (event) {
        event.preventDefault();
        var send = $(this);
        send.prop("disabled", true);
        var form = $("#edit-whyUsSection-image-form")[0];
        var formData = new FormData(form);
        $.ajax({
            url: applyEditWhyUsImage,
            type: 'Post',
            processData: false,
            contentType: false,
            data: formData,
            success: function (res) {
                if (res.success) {
                    form.reset();
                    location.reload();
                } else {
                    handleApiError(res.error);
                    send.prop("disabled", false);
                }
            },
            error: function () {
                handleApiError("در بارگذاری فرم ویرایش خطایی چیش امده");
                send.prop("disabled", false);
            }
        });
    });
});