$(() => {
    $(document).on("change", ".input-about-us-logo", function () {
        var preview = $(".preview-add-image-about-us-logo")[0];
        var inputLogo = $(".input-about-us-logo")[0];
        setUpImagePreview(inputLogo, preview);
    })

    $(document).on("click", "#send-about-us-button", function (e) {
        e.preventDefault();
        var form = $("#add-about-us-section-form")[0];
        var formData = new FormData(form);

        ajaxWithButtonLoading({
            button: "#send-about-us-button",
            url: createAboutUs,
            type: 'Post',
            data: formData,
            contentType: false,
            processData: false,
            success: function (res) {
                location.reload();
            }
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

        ajaxWithButtonLoading({

            button: "#edit-about-us-button",
            url: getAboutUsForEdit,
            type: 'GET',
            data: { id: id },
            success: function (res, status, xhr) {
                var modalEl = document.getElementById('staticBackdrop');
                // قرار دادن html پارشیال در body مودال
                $("#staticBackdrop .modal-body").html(res);
                // نمایش مودال
                var modal = new bootstrap.Modal(modalEl);
                modal.show();
                modalEl.addEventListener('shown.bs.modal', function () {
                    var editLatitude = $("#edit-latitude").val();
                    var editLongitude = $("#edit-longitude").val();
                    ShowMapForEdit(editLatitude, editLongitude);
                },
                    {
                        once: true
                    });
                modalEl.addEventListener('hidden.bs.modal', function () {
                    if (window._editMap) {
                        try {
                            window._editMap.remove();
                        } catch (e) {
                            /* ignore */
                        }
                        window._editMap = null;
                    }
                },
                    {
                        once: true
                    });

            }
        });


       
    });

    $(document).on("click", "#apply-edit-about-us-button", function (e) {
        e.preventDefault();
        var btn = $(this);
       
        var form = $("#edit-about-us-section-form")[0]
        var formData = new FormData(form);

        ajaxWithButtonLoading({
            button: "#apply-edit-about-us-button",
            url: applyEditAboutUs,
            type: 'Post',
            contentType: false,
            processData: false,
            data: formData,
            success: function (res)
            {
                location.reload();
            }
        });
    })

    $(document).on("click", ".edit-about-us-logo-button", function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        ajaxWithButtonLoading({
            button: this,
            url: getLogoByAboutUsId,
            data: { id: id },
            type: 'Get',
            success: function (res, status, xhr)
            {
                var modalEl = document.getElementById('staticBackdrop');
                $("#staticBackdrop .modal-body").html(res);
                var modal = new bootstrap.Modal(modalEl);
                modal.show();
            }
        });
    })

    $(document).on("click", "#apply-edited-about-us-logo", function (e) {
        e.preventDefault();
        var form = $("#edit-about-us-logo-form")[0];
        var formData = new FormData(form);
        ajaxWithButtonLoading({
            button: "#apply-edited-about-us-logo",
            url: applyEditedaboutUsLogo,
            type: 'Post',
            data: formData,
            contentType: false,
            processData: false,
            success: function (res)
            {
                location.reload();
            }
        });

        //$.ajax({
        //    url: applyEditedaboutUsLogo,
        //    type: 'Post',
        //    data: formData,
        //    contentType: false,
        //    processData: false,
        //    success: function (res) {
        //        if (res.success) {
        //            location.reload();
        //        } else {
        //            handleApiError(res.error);
        //            btnSend.prop("disabled", false);
        //        }
        //    },
        //    Error: function () {
        //        handleApiError("در بارگذاری فرم ویرایش خطایی پیش امده");
        //        btnSend.prop("disabled", false);
        //    }
        //});
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