$(() => {

    $(document).on("change", ".whyus-section-image-input", function () {
        var inputImage = $(".whyus-section-image-input")[0];
        var previewImage = $(".whyus-section-preview-img")[0];
        setUpImagePreview(inputImage, previewImage);
    })

    $(document).on("click", "#createWhyUsSection", function (e) {
        e.preventDefault();
        var form = $("#add-whyus-section-form")[0];
        var formData = new FormData(form);

        ajaxWithButtonLoading({
            button: "#createWhyUsSection",
            url: createWhyUsSectionUrl,
            type: 'Post',
            data: formData,
            contentType: false,
            processData: false,
            success: function (res) {
                location.reload();
            }
        })
    })

    $(document).on("click", ".add-why-us-question-btn", function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        ajaxWithButtonLoading({
            button: this,
            url: addWhyUsQuestionPartialUrl,
            type: 'Get',
            data: { id: id },
            success: function (res, status, xhr)
            {
                $("#staticBackdrop .modal-body").html(res);
                var modal = new bootstrap.Modal(document.getElementById('staticBackdrop'));
                modal.show();
            }
        });
    });

    $(document).on("click", "#add-why-us-question", function (e) {
        e.preventDefault();
        var form = $("#add-why-us-question-form")[0];
        var formData = new FormData(form);
        ajaxWithButtonLoading({
            button: "#add-why-us-question",
            url: addQuestionsUrl,
            type: 'Post',
            data: formData,
            processData: false,
            contentType: false,
            success: function (res)
            {
                location.reload();
            }
        });
    })

    $(document).on("click", ".delete-question-btn", function (e) {
        e.preventDefault();
        var removeBtn = $(this);
        var id = removeBtn.data("id");
        var token = $('input[name="__RequestVerificationToken"]').val();
        ajaxWithButtonLoading({
            button: this,
            url: deleteQuestionUrl,
            type: 'Post',
            data: {
                id: id,
                __RequestVerificationToken: token
            },

            success: function (res)
            {
                location.reload();
            }
        });
    });

    $(document).on("click", ".edit-why-us-section-btn", function (e) {
        e.preventDefault();
        var btnSend = $(this);
        id = $(this).data("id");
        ajaxWithButtonLoading({
            button: this,
            url: editWhyUsSectionEditUrl,
            type: 'Get',
            data: { id: id },
            success: function (res, status, xhr) {
                $("#staticBackdrop .modal-body").html(res);
                var modal = new bootstrap.Modal(document.getElementById('staticBackdrop'));
                modal.show();
            }
        });
     
    });

    $(document).on("click", ".edit-why-us-section-title-description", function (e) {
        e.preventDefault();
        var form = $("#edit-why-us-section-title-description-form")[0];
        var formData = new FormData(form);
        ajaxWithButtonLoading({

            button: this,
            url: applyEditTitleAndDescripWhyUsSection,
            type: 'Post',
            processData: false,
            contentType: false,
            data: formData,
            success: function (res) {
                location.reload();
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
        var form = $("#edit-whyUsSection-image-form")[0];
        var formData = new FormData(form);

        ajaxWithButtonLoading({
            button: this,
            url: applyEditWhyUsImage,
            type: 'Post',
            processData: false,
            contentType: false,
            data: formData,
            success: function (res) {
                location.reload();
            }

        });
    });
});

document.querySelectorAll(".faq-item").forEach(item => {
    const question = item.querySelector(".faq-question");
    const answer = item.querySelector(".faq-answer");
    const toggle = item.querySelector(".faq-toggle");

    question.addEventListener("click", () => {
        answer.classList.toggle("open"); // باز و بسته کردن
        toggle.textContent = answer.classList.contains("open") ? "−" : "+"; // تغییر آیکون
    });
});
