
function setUpImagePreview(inputSelector, previewSelector) {
    const input = inputSelector;
    if (!validateImageFile(input)) return;
    if (input.files && input.files[0]) {

        const reader = new FileReader();
        reader.onload = function (e) {
            $(previewSelector).attr("src", e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}


function setUpImagePreviewFixed(input, preview) {
    if (!validateImageFile(input)) return;

    if (input.files && input.files[0]) {
        const reader = new FileReader();
        reader.onload = function (e) {
            $(preview).attr("src", e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}


function validateImageFile(input) {

    const file = input.files[0];
    const allowedExtension = /(\.jpg|\.jpeg|\.png)$/i;


    if (!file) return false;
    if (!allowedExtension.exec(file.name)) {
        showPopup("فرمت تصویر باید jpg، jpeg یا png باشد.");
        input.value = "";
        markInputError(input);
        return false;
    }
    return true;
}

function markInputError(input) {
    input.classList.add("input-error");
    setTimeout(() => input.classList.remove("input-error"), 5000);
}

//$(document).ajaxError(function (event, jqxhr) {
//    if (jqxhr.status === 0) {
//        // اینترنت قطع است
//        window.location.href = '/Error';
//        return;
//    }

//    try {
//        var json = jqxhr.responseJSON || JSON.parse(jqxhr.responseText);
//        if (json && json.redirect) {
//            window.location.href = json.redirect;
//            return;
//        }
//    } catch { }

//    // در غیر این صورت به صفحه خطا برو
//    window.location.href = '/Error';
//});


$(document).ajaxError(function (event, xhr, settings, error) {
    if (xhr.status === 401 || xhr.responseText === "SessionExpired") {
        const returnUrl = encodeURIComponent(window.location.pathname + window.location.search);
        window.location.href = `/Auth/Login?returnUrl=${returnUrl}`;
        return;
    }
});



function ajaxWithButtonLoading(options) {
    var btn = $(options.button);
    if (btn.prop("disabled")) return;

    var oldText = btn.text();

    btn.prop("disabled", true);
    btn.html("<span class='spinner'></span> در حال پردازش...");

    $.ajax({
        url: options.url,
        type: options.type,
        data: options.data,
        contentType: options.contentType === false ? false : "application/x-www-form-urlencoded; charset=UTF-8",
        processData: options.processData !== undefined ? options.processData : true,

        success: function (res, status, xhr) {
            const contentType = xhr.getResponseHeader("content-type") || "";

            // --- 🔥 اگر JSON بود → چک کنیم ---
            if (contentType.includes("application/json")) {
                if (res.success === false) {
                    if (typeof handleApiError === "function") handleApiError(res.error);
                    return;
                }
            }

            // --- در باقی موارد → نتیجه عادی را به کاربر بده ---
            if (options.success) options.success(res, status, xhr);
        },

        error: function (err) {
            if (typeof handleApiError === "function") handleApiError(err);
            if (options.error) options.error(err);
        },

        complete: function () {
            btn.prop("disabled", false);
            btn.html(oldText);
            if (options.complete) options.complete();
        }
    });
}



var errorMessages = {};

fetch('/config/exception.json')
    .then(res => res.json())
    .then(data => { errorMessages = data; })
    .catch(err => console.error("خطا در بارگذاری فایل JSON:", err));

function handleApiError(errorKey) {
    let message = errorMessages[errorKey] || errorKey;
    showPopup(message);
    return message;
}

function showPopup(msg) {
    if (window.toastr) {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            positionClass: "toast-top-right",
            timeOut: 8000,
            newestOnTop: true,
            preventDuplicates: true,
            rtl: true
        };
        toastr.error(msg, "خطا");
    } else {
        alert(msg);
    }
}


$(function () {
    var error = $("#error-input").val();
    if (error) handleApiError(error);
});