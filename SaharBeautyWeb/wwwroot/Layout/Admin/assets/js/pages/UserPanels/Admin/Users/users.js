$(document).ready(function () {
    $("#user-search").on("input", function () {
        // وقتی طول ورودی >= 3 یا خالی شد، فرم را submit کن
        if ($(this).val().length >= 3 || $(this).val().length === 0) {
            $("#search-form")[0].submit();
        }
    });
});