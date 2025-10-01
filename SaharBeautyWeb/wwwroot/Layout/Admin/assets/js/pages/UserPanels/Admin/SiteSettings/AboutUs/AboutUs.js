$(() => {
    $(document).on("input", "#about-us-mobile", function () {
        var mobile = $(this)[0];
        var sendButton = $("#send-about-us-button");
        if (!mobile.value.trim()) {
            markInputError(mobile);
            sendButton.prop("disabled", true);
            return;
        }
        sendButton.prop("disabled", false);
    })

    $(document).on("input", "#input-about-us-logo", function () {
        setUpImagePreview("#input-about-us-logo", "#preview-add-image-about-us-logo");
    })

    $(document).on("click", "#send-about-us-button", function (e) {
        e.preventDefault();
        var form = $("#add-about-us-section-form")[0];
        var formData = new FormData(form);
        $.ajax({
            url: createAboutUs,
            type: 'Post',
            data: formData,
            contentType: false,
            processData: false,
            success: function (res) {
                console.log(res);
                if (res.success) {
                    location.reload();

                } else {
                    handleApiError(res.error);
                    return;
                }
            },
        });
    });

    var map = L.map('map').setView([35.6892, 51.3890], 13); // مرکز تهران، می‌توانید تغییر دهید

    // اضافه کردن Tile Layer از OpenStreetMap
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map);

    var marker;

    map.on('click', function (e) {
        var lat = e.latlng.lat.toFixed(6);
        var lng = e.latlng.lng.toFixed(6);

        // اگر قبلا مارکر بود، حذف کن
        if (marker) {
            map.removeLayer(marker);
        }

        // اضافه کردن مارکر جدید
        marker = L.marker([lat, lng]).addTo(map);

        // مقدار hidden input ها را ست کن
        document.getElementById('latitude').value = lat;
        document.getElementById('longitude').value = lng;
    });


});