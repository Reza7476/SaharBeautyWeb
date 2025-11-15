
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

