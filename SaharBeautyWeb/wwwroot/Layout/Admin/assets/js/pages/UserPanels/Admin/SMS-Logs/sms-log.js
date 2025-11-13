

$("#searchBox").on("input", function () {

    const value = $(this).val().trim();

    if (value === "") {
        // اگر سرچ پاک شد، مقدار فیلد را خالی کن و فرم را بفرست
        $(this).val("");
    }

    $("#search-form")[0].submit();
});