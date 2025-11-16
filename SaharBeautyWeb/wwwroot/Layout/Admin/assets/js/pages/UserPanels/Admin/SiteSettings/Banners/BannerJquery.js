$(() => {
    $(document).on("change", ".get-banner-image", function () {
        var imageInput = $(".get-banner-image")[0];
        var viewImg = $(".preview-new-banner-image")[0];
        setUpImagePreview(imageInput, viewImg);
    })

    $(document).on("click","#createBanner" ,function (e) {
        e.preventDefault();
        var btnSend = $(this);
        var imageInput = $(".get-banner-image")[0];
        var viewImg = $(".preview-new-banner-image")[0];
        setUpImagePreview(imageInput, viewImg);
    
        var form = $("#add-banner-form")[0]
        let formData = new FormData(form);

        btnSend.prop("disabled", true);
        $.ajax({
            url: createBanner,
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: function () {
                btnSend.prop("disabled", true);
            },
            success: function (res) {
                if (res.success) {
                    $("#add-banner-form")[0].reset();
                    location.reload();
                } else {
                    handleApiError(res.error);
                    btnSend.prop("disabled", false);
                    return
                }
            },
            error: function (xhr) {

                handleApiError(xhr);
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

  

    $(document).on("click", "#saveBannerBtn", function () {

        var btnSend = $(this);
        var imageInput = $(".get-banner-image")[0];
        var viewImg = $(".preview-new-banner-image")[0];
        setUpImagePreview(imageInput, viewImg);

        var form = $("#editBannerForm")[0];
        var formData = new FormData(form);

        $.ajax({
            url: editBanner,
            type: 'Post',
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: function () {
                btnSend.prop("disabled", true);
            },
            success: function (res) {
                if (res.success) {
                    var modalEl = document.getElementById('staticBackdrop');
                    var modal = bootstrap.Modal.getInstance(modalEl);
                    modal.hide();
                    $("#editBannerForm")[0].reset();
                    location.reload();
                } else {
                    handleApiError(res.error);
                    btnSend.prop("disabled", false);
                }
            },
            error: function () {
                showPopup("خطایی رخ داده");
                var modal = new bootstrap.Modal(document.getElementById('staticBackdrop'));
                modal.hide();
            },
            complete: function () {
                 btnSend.prop("disabled", false);
            }
        });
    });

});
