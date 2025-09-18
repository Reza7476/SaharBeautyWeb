$(() => {
    setUpImagePreview("#input-why-us-section-image", "#preview-add-image-why-us-section");
    $(document).on("click", "#createWhyUsSection", function (e) {
        e.preventDefault();
        var send = $(this);
        var titleInput = $("#why-us-section-title")[0];
        var descriptionInput = $("#why-us-description")[0];
        var imageInput = $("#input-why-us-section-image")[0];
        let hasError;
        if (!titleInput.value.trim()) {
            markInputError(titleInput);
            hasError = true;
        }
        if (!descriptionInput.value.trim()) {
            markInputError(descriptionInput);
            hasError = true;

        }
        if (!(imageInput.files[0])) {
            markInputError(imageInput);
            hassError = true;
        }
        if (hasError) return;
        var form = $("#add-why-us-section-form")[0];
        var formData = new FormData(form);
        $.ajax({
            url: createWhyUsSectionUrl,
            type: 'Post',
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: function () {
                send.prop("disabled", true);
            },
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
        console.log(id);
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
        var question = $("#why-us-question")[0];
        var answer = $("#why-us-question-answer")[0];
        var hasError;
        if (!question.value.trim()) {
            markInputError(question);
            hasError = true;
        }
        if (!answer.value.trim()) {
            markInputError(answer);
            hasError = true;
        }
        if (hasError) return;
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

        var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            url: deleteQuestionUrl,
            type: 'Post',
            data: {
                id: id,
                __RequestVerificationToken: token
            },
            beforeSend: function () {
                removeBtn.prop("disabled", true);
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
        id = $(this).data("id");
        $.ajax({
            url: editWhyUsSectionEditUrl,
            type: 'Get',
            data: { id: id },
            success: function (html) {
                var modal = new bootstrap.Modal(document.getElementById('staticBackdrop'));
                $("#staticBackdrop .modal-body").html(html);
                modal.show();
            },
            error: function (xhr, status, error) {
                let msg = (xhr.responseJSON?.error) || xhr.responseText || "خطایی پیش آمده";
                showPopup(msg);
            }

        });
    });

    $(document).on("click", ".edit-why-us-section-title-description", function (e) {
        e.preventDefault();
        var sendBtn = $(this);
        var title = $("#edit-why-us-section-title")[0];
        var description = $("#edit-why-us-section-description")[0];
        let hasError;
        if (!title.value.trim()) {
            markInputError(title);
            hasError = true;
        }
        if (!description.value.trim()) {
            markInputError(description);
            hasError = true;
        }
        if (hasError) return;
        var form = $("#edit-why-us-section-title-description-form")[0];
        var formData = new FormData(form);

        $.ajax({
            url: applyEditTitleAndDescripWhyUsSection,
            type: 'Post',
            processData: false,
            contentType: false,
            data: formData,
            befoerSend: function () {
                sendBtn.prop("disabled", true);
            },
            success: function (res) {
                if (res.success) {
                    location.reload();
                } else {
                    handleApiError(res.error);
                    location.reload();
                }
            },
            error: function (xhr) {
                let msg = (xhr.responseJSON?.error) || xhr.responseText || "خطایی پیش آمده";
                handleApiError(msg);
                location.reload();
            }
        });

    });


});