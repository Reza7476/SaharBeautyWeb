function setUpImagePreview(inputSelector, previewSelector) {
    $(document).on("change", inputSelector, function () {
        const input = this;
        if (!validateImageFile(input)) return;
        if (input.files && input.files[0]) {

            const reader = new FileReader();
            reader.onload = function (e) {
                $(previewSelector).attr("src", e.target.result);
            };
            reader.readAsDataURL(input.files[0]);
        }
    });
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