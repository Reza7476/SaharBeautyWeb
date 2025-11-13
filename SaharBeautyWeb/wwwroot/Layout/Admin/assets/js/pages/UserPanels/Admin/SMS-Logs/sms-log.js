

$("#searchBox").on("input", function () {

    const value = $(this).val().replace(/\D/g, "");
    $(this).val(value);
    if (value.length === 11) {
        $("#search-form")[0].submit();
    } else if (value.length === 0) {
        $("#search-form")[0].submit();
    }
});