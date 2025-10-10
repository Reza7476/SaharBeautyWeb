$(() => {
    $(document).on("click","#add-treatment-btn", function (e) {
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
        var sendBtn = $(this);
        sendBtn.prop("disabled", true);
        var title = $("#add-treatment-title").val();
        var image = $(".add-treatment-image-input")[0];
        var description = $("#add-treatment-description").val();

        if (!title || (!image.files || image.files.length === 0) || !description) {
            showPopup("فیلد ها نباید خالی باشند");
            return;
        }
       var form = $("#add-treatment-form")[0];
       var formData = new FormData(form);
       $.ajax({
           url: addTreatment,
           type: 'Post',
           data: formData,
           contentType: false,
           processData: false,
           beforeSend: function () {
               sendBtn.prop("disabled", true);
           },
           success: function (res) {
               if (res.success) {

                   var modalEl = document.getElementById('staticBackdrop');
                   var modal = bootstrap.Modal.getInstance(modalEl);
                   modal.hide();
                   form.reset();
                   location.reload();
               } else {
                   handleApiError(res.error );
                   sendBtn.prop("disabled", false);
               }
           },
           error: function () {
               showPopup("خطایی رخ داده")
               var modal = new bootstrap.Modal(document.getElementById('staticBackdrop'));
               modal.hide();
               location.reload();
           }
       });
    })

    $(".edit-treatment-btn").on("click", function () {
        var id = $(this).data("id");
        $.ajax({
            url: editTreatmentPartial,
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
            error: function () {
                showPopup("خطا در بارگذاری فرم ویرایش!");
                btnSend.prop("disabled", false);
            },
            complete: function () {

                btnSend.prop("disabled", false);
            }
        });
    });

    //$(document).on("change", "#edit-treatment-image-input", function () {
    //    var input = this;
    //    if (input.files && input.files[0]) {
    //        var reader = new FileReader();
    //        reader.onload = function (e) {
    //            $("#preview-edit-image-treatment").attr("src", e.target.result);
    //        }
    //    }
    //    reader.readAsDataURL(input.files[0]);
    //});

    //$(document).on("click", ".add-edited-treatment-image", function (e) {
    //    e.preventDefault();
    //    var sendBtn = $(this);
    //    var form = $("#add-treatment-image-form")[0];
    //    var formData = new FormData(form);

    //    $.ajax({
    //        url: '@Url.Page("Index", "AddImage")',
    //        type: 'Post',
    //        data: formData,
    //        contentType: false,
    //        processData: false,
    //        beforeSend: function () {
    //            sendBtn.prop("disabled", true);
    //        },
    //        success: function (res) {
    //            if (res.success) {
    //                var modalEl = document.getElementById('staticBackdrop');
    //                var modal = bootstrap.Modal.getInstance(modalEl);
    //                modal.hide();
    //                $("#add-treatment-image-form")[0].reset();
    //                location.reload();
    //            } else {
    //                handleApiError(res.error);
    //                sendBtn.prop("disabled", false);
    //            }
    //        },
    //        error: function (res) {
    //            showPopup(res.error);
    //            sendBtn.prop("disabled", false);
    //        }
    //    });
    //});

    //$(document).on("click", ".delete-treatment-image-btn", function (e) {
    //    e.preventDefault();
    //    var deleteBtn = $(this);
    //    var token = $('input[name="__RequestVerificationToken"]').val();
    //    var treatmentId = $("#treatmentId").val();
    //    imageId = deleteBtn.data("id");
    //    $.ajax({
    //        url: '@Url.Page("Index", "DeleteImage")',
    //        type: 'Post',
    //        data: {
    //            imageid: imageId,
    //            id: treatmentId,
    //            __RequestVerificationToken: token,
    //        },
    //        success: function (res) {

    //            if (res.success) {
    //                deleteBtn.closest(".col-md-6").remove();
    //            } else {
    //                handleApiError(res.error)
    //            }
    //        },
    //        error: function () {
    //            showPopup("خطایی ایجاد شده");
    //        }
    //    });
    //})

    //$(document).on("click", ".edit-treatment-title-description", function (e) {
    //    e.preventDefault();
    //    var sendBtn = $(this);
    //    var title = $("#edit-treatment-title").val();
    //    var description = $("#edit-treatment-description").val();
    //    if (!(title || description)) {
    //        showPopup("عنوان و توضیحات نباید خالی باشند")
    //    }
    //    var form = $("#edit-treatment-title-description-form")[0];
    //    var formData = new FormData(form);
    //    $.ajax({
    //        url: '@Url.Page("Index", "EditTreatment")',
    //        type: 'Post',
    //        data: formData,
    //        processData: false,
    //        contentType: false,
    //        success: function (res) {
    //            if (res.success) {
    //                var modalEl = document.getElementById('staticBackdrop');
    //                var modal = bootstrap.Modal.getInstance(modalEl);
    //                modal.hide();
    //                $("#edit-treatment-title-description-form")[0].reset();
    //                location.reload();
    //            } else {
    //                handleApiError(res.erro)
    //            }
    //        },
    //        error: function () {
    //            showPopup("خطایی پیش آمده")
    //        }
    //    });
    //})

})