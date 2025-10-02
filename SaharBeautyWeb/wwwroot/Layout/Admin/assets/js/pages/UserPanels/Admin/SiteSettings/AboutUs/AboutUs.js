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

    $(document).on("click", "#edit-about-us-button", function () {
        var id = $("#about-us-id").val();
        $.ajax({
            url: getAboutUsForEdit,
            type: 'GET',
            data: { id: id },
            success: function (html) {
                if (html) {
                    var modalEl = document.getElementById('staticBackdrop');

                    // قرار دادن html پارشیال در body مودال
                    $("#staticBackdrop .modal-body").html(html);

                    // نمایش مودال
                    var modal = new bootstrap.Modal(modalEl);
                    modal.show();

                    // وقتی مودال کامل باز شد ⇒ نقشه را مقداردهی کن
                    modalEl.addEventListener('shown.bs.modal', function () {
                        var editLatitude = $("#edit-latitude").val();
                        var editLongitude = $("#edit-longitude").val();
                        ShowMapForEdit(editLatitude, editLongitude);
                    }, { once: true });

                    // وقتی مودال بسته شد ⇒ نقشه قبلی را پاک کن تا بار بعد تداخلی نباشه
                    modalEl.addEventListener('hidden.bs.modal', function () {
                        if (window._editMap) {
                            try { window._editMap.remove(); } catch (e) { /* ignore */ }
                            window._editMap = null;
                        }
                    }, { once: true });

                } else {
                    handleApiError("پاسخی دریافت نشد");
                    return;
                }
            },
            error: function (xhr) {
                let msg = (xhr.responseJSON?.error) || xhr.responseText || "خطایی پیش آمده";
                showPopup(msg);
            }
        });
    });

    $(document).on("click", "#apply-edit-about-us-button", function (e) {
        e.preventDefault();
        var btn = $(this);
        var mobile = $("#edit-about-us-mobile")[0];
        if (!mobile.value.trim()) {
            markInputError(mobile);
            btn.prop("disabled", true);
            return;
        }
        var form = $("#edit-about-us-section-form")[0]
        var formData = new FormData(form);
        $.ajax({
            url: applyEditAboutUs,
            type: 'Post',
            contentType: false,
            processData: false,
            data: formData,
            success: function (res) {
                if (res.success) {
                    location.reload();
                } else {
                    handleApiError(res.error);
                    location.reload();
                }
            }

        });
    })




});

function ShowMapForEdit(lat, lon) {
    var salaonLat = parseFloat(lat);
    var salonLng = parseFloat(lon);

    var centerLat = isFinite(salaonLat) ? salaonLat : 29.6100;
    var centerLng = isFinite(salonLng) ? salonLng : 52.5400;

    if (window._editMap) {
        try { window._editMap.remove(); } catch (e) { }
        window._editMap = null;
    }

    var map = L.map('edit-display-salon-map', {
        dragging: true,
        touchZoom: true,
        scrollWheelZoom: true,
        doubleClickZoom: true,
        keyboard: true,
        zoomControl: true
    }).setView([centerLat, centerLng], 15);

    window._editMap = map;

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map);

    var latInput = document.getElementById('edit-latitude');
    var lngInput = document.getElementById('edit-longitude');

    var marker = null;

    // اگر مختصات معتبر بود ⇒ مارکر اولیه را بگذار
    if (isFinite(salaonLat) && isFinite(salonLng)) {
        marker = L.marker([salaonLat, salonLng], { draggable: true }).addTo(map);
        latInput.value = salaonLat;
        lngInput.value = salonLng;

        marker.on('dragend', function (e) {
            var pos = e.target.getLatLng();
            latInput.value = pos.lat.toFixed(6);
            lngInput.value = pos.lng.toFixed(6);
        });
    } else {
        latInput.value = "";
        lngInput.value = "";
    }

    // کلیک روی نقشه ⇒ مارکر جدید یا جابه‌جایی
    map.on('click', function (e) {
        var lat = e.latlng.lat.toFixed(6);
        var lng = e.latlng.lng.toFixed(6);

        if (!marker) {
            marker = L.marker([lat, lng], { draggable: true }).addTo(map);
            marker.on('dragend', function (e) {
                var pos = e.target.getLatLng();
                latInput.value = pos.lat.toFixed(6);
                lngInput.value = pos.lng.toFixed(6);
            });
        } else {
            marker.setLatLng([lat, lng]);
        }

        latInput.value = lat;
        lngInput.value = lng;
    });

    // دکمه حذف مکان
    $(document).off("click", "#remove-location-btn").on("click", "#remove-location-btn", function () {
        if (marker) {
            map.removeLayer(marker);
            marker = null;
        }
        latInput.value = "";
        lngInput.value = "";
    });

    setTimeout(() => { map.invalidateSize(); }, 200);
}



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
        zoomControl: true
    }).setView([salaonLat, salonLng], 15); // زوم نزدیک

    // اضافه کردن لایه Tile
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map12);

    // اضافه کردن مارکر روی نقطه
    L.marker([salaonLat, salonLng]).addTo(map12);
}