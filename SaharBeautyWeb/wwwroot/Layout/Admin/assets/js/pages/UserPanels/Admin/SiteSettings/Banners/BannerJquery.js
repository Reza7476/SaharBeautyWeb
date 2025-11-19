$(() => {
    $(document).on("change", ".get-banner-image", function () {
        var imageInput = $(".get-banner-image")[0];
        var viewImg = $(".preview-new-banner-image")[0];
        setUpImagePreview(imageInput, viewImg);
    })

    $(document).on("click","#createBanner" ,function (e) {
        e.preventDefault();
        var imageInput = $(".get-banner-image")[0];
        var viewImg = $(".preview-new-banner-image")[0];
        setUpImagePreview(imageInput, viewImg);
    
        var form = $("#add-banner-form")[0]
        let formData = new FormData(form);
        ajaxWithButtonLoading({
            button: "#createBanner",
            url: createBanner,
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (res) {
                location.reload();
            }
        })
    });

    $(".edit-banner-btn").on("click", function () {
        var bannerId = $(this).data("id");
        var btnSend = $(this);

        ajaxWithButtonLoading({
            button: this,// $(this),//".edit-banner-btn",
            url: editBannerPartial,
            type: 'GET',
            data: { id: bannerId },
            success: function (res, status, xhr) {
                // در این نقطه فقط وقتی HTML معتبر بود وارد می‌شود
                $("#staticBackdrop .modal-body").html(res);
                new bootstrap.Modal(document.getElementById('staticBackdrop')).show();
            },
        });
    })

    $(document).on("click", "#saveBannerBtn", function () {

        var btnSend = $(this);
        var imageInput = $(".get-banner-image")[0];
        var viewImg = $(".preview-new-banner-image")[0];
        setUpImagePreview(imageInput, viewImg);

        var form = $("#editBannerForm")[0];
        var formData = new FormData(form);
        ajaxWithButtonLoading({
            button: "#saveBannerBtn",
            url: editBanner,
            type: 'Post',
            data: formData,
            contentType: false,
            processData: false,
            success: function (res)
            {
                var modalEl = document.getElementById('staticBackdrop');
                var modal = bootstrap.Modal.getInstance(modalEl);
                modal.hide();
                location.reload();
            }
        });
    });

});
