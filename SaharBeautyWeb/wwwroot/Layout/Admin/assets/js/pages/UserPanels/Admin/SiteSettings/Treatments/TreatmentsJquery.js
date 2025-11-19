$(() => {
    $(document).on("click", "#add-treatment-btn", function (e) {
        e.preventDefault();
        $.ajax({
            url: addTreatmentPartial,
            type: 'Get',
            success: function (html) {
                var modal = new bootstrap.Modal(document.getElementById('staticBackdrop'));
                $("#staticBackdrop .modal-body").html(html);
                modal.show();
            },
            error: function () {
            }
        })
    })


    $(document).on("change", ".add-treatment-image-input", function () {
        var imageInput = $(".add-treatment-image-input")[0];
        var viewImg = $(".preview-new-image-treatment")[0];
        setUpImagePreview(imageInput, viewImg);

    })


    $(document).on("click", "#createTreatment", function (e) {
        e.preventDefault();
        var title = $("#add-treatment-title").val();
        var image = $(".add-treatment-image-input")[0];
        var description = $("#add-treatment-description").val();

        if (!title || (!image.files || image.files.length === 0) || !description) {
            showPopup("فیلد ها نباید خالی باشند");
            return;
        }
        var form = $("#add-treatment-form")[0];
        var formData = new FormData(form);

        ajaxWithButtonLoading({
            button: "#createTreatment",
            url: addTreatment,
            type: 'Post',
            data: formData,
            contentType: false,
            processData: false,
            success: function (res) {
                // در این نقطه فقط وقتی HTML معتبر بود وارد می‌شود
                var modalEl = document.getElementById('staticBackdrop');
                var modal = bootstrap.Modal.getInstance(modalEl);
                modal.hide();
                location.reload();
            },
        });
    })

    $(document).on("click", ".edit-treatment-title-description", function (e) {
        e.preventDefault();
        var sendBtn = $(this);
        var title = $("#edit-treatment-title").val();
        var description = $("#edit-treatment-description").val();
        if (!(title || description)) {
            showPopup("عنوان و توضیحات نباید خالی باشند")
        }
        var form = $("#edit-treatment-title-description-form")[0];
        var formData = new FormData(form);
        ajaxWithButtonLoading({
            button: this,
            url: editTreatment,
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

    $(".edit-treatment-btn").on("click", function () {
        var id = $(this).data("id");

        ajaxWithButtonLoading({
            button: this,
            url: editTreatmentPartial,
            type: 'Get',
            data: { id: id },
            success: function (res, status, xhr) {
                // در این نقطه فقط وقتی HTML معتبر بود وارد می‌شود
                $("#staticBackdrop .modal-body").html(res);
                new bootstrap.Modal(document.getElementById('staticBackdrop')).show();
            },
        })
    });



    $(document).on("click", ".add-edited-treatment-image", function (e) {
        e.preventDefault();
        var form = $("#add-treatment-image-form")[0];
        var formData = new FormData(form);

        ajaxWithButtonLoading({
            button: this,
            url: addImage,
            type: 'Post',
            data: formData,
            contentType: false,
            processData: false,
            success: function (res) {
                location.reload();
            }
        });
    });

    $(document).on("click", ".delete-treatment-image-btn", function (e) {
        e.preventDefault();
        var btn = $(this);
        var token = $('input[name="__RequestVerificationToken"]').val();
        var treatmentId = $("#treatmentId").val();
        imageId = $(this).data("id");
        ajaxWithButtonLoading({
            button: this,
            url: deleteImage,
            type: 'Post',
            data: {
                imageid: imageId,
                id: treatmentId,
                __RequestVerificationToken: token,
            },
            success: function (res) {
                btn.closest(".col-md-6").remove();
            }
        });
    })


    setupPriceFormatter("#add-treatment-price")
    setupPriceFormatter("#edit-treatment-price")
})
function setupPriceFormatter(selector) {
    $(document).on("input", selector, function () {
        var input = $(this);
        let value = input.val();

        // حذف کاراکترهای غیرعددی (فارسی و انگلیسی)
        value = value.replace(/[^0-9۰-۹]/g, '');

        // تبدیل اعداد فارسی به انگلیسی
        const persianDigits = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
        for (let i = 0; i < 10; i++) {
            value = value.replace(new RegExp(persianDigits[i], 'g'), i);
        }

        // فرمت کردن به سبک سه‌رقم سه‌رقم (فارسی)
        if (value.length > 0 && $.isNumeric(value)) {
            let formatted = Number(value).toLocaleString('fa-IR');
            input.val(formatted);
        } else {
            input.val('');
        }
    });
}
