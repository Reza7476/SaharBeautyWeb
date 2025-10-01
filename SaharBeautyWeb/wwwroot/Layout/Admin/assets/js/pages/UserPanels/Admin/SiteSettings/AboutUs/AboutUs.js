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


    if ($("#map").length) {
        GetMap();
    }
    if ($("#display-salon-map").length) {
        var lat = $("#salon-latitude").val();
        var long = $("#salon-longitude").val()
        ShowMap(lat, long);
    }
});



function GetMap() {

    var map = L.map('map').setView([29.6100, 52.5400], 13); // مرکز شیراز، می‌توانید تغییر دهید

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
}
function ShowMap(lat, long) {
    var salaonLat = lat || 29.6100;
    var salonLng = long || 52.5400;

    // ایجاد نقشه روی مختصات
    var map12 = L.map('display-salon-map', {
        dragging: false,   // غیر فعال کردن حرکت نقشه
        touchZoom: true,
        scrollWheelZoom: true,
        doubleClickZoom: false,
        boxZoom: false,
        keyboard: false,
        zoomControl:true
    }).setView([salaonLat, salonLng], 15); // زوم نزدیک

    // اضافه کردن لایه Tile
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map12);

    // اضافه کردن مارکر روی نقطه
    L.marker([salaonLat, salonLng]).addTo(map12);
}