$(document).on("click", "#add-new-client-btn", function (e) {
    alert('dfjasdf');
    e.preventDefault();
    var form = $("#registerForm")[0];
    var formData = new FormData(form);

    ajaxWithButtonLoading({
        button: "#add-new-client-btn",
        url: rejesterClient,
        type: 'post',
        contentType: false,
        processData: false,
        data:  formData ,
        success: function (res) {
            if (res.redirectUrl && res.redirectUrl.trim() !== "") {
                window.location.href = res.redirectUrl;
            } else {
                window.location.href = '/UserPanels/Admin/Index';
            }
           
        }

    });
});