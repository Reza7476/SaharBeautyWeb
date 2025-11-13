

$("#searchBox").on("input", function () {

    const value = $(this).val().trim();

    if (value.length >= 11) {
        $("#search-form")[0].submit();
    }
});