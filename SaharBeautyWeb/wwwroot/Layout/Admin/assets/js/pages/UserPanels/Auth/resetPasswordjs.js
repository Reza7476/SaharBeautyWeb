function toEnglishDigits(str) {
    if (!str) return "";
    return str
        .replace(/[\u06F0-\u06F9]/g, d => String.fromCharCode(d.charCodeAt(0) - 1728)) // فارسی
        .replace(/[\u0660-\u0669]/g, d => String.fromCharCode(d.charCodeAt(0) - 1584)); // عربی
}

$(document).on("click", "#step-one-btn", function (e) {
    e.preventDefault();
    var errorP = $("#error-register");
    var otpRequestId = $("#Otp-request-id")[0];
    var form = $("#step-one-form")[0];
    var formData = new FormData(form);
    //var otpBtn = $(this);
    //otpBtn.prop("disabled", true);
    ajaxWithButtonLoading({
        button: "#step-one-btn",
        url: sendOtp,
        type: 'Post',
        data: formData,
        contentType: false,
        processData: false,
        success: function (res)
        {
            otpRequestId.value = res.data.otpRequestId;
            $("#step-one-form").removeClass("active");
            $("#step-two-form").addClass("active");

            resetOtpTimer();
            startTimer(120);
        }
    });
});

getOtpCods();

$(document).on("click", "#verify-otp-btn", function (e) {
    e.preventDefault();
    const formStepOne = $("#step-one-form")[0]
    const form = $("#step-two-form")[0];
    const formData = new FormData(form);
    var errorP = $("#error-register");
    var otpBtn = $("#step-one-btn");
    $("#step-two-form span[asp-validation-for]").text("");

    $.ajax({
        type: "POST",
        url: verifyOtp,
        data: formData,
        processData: false,
        contentType: false,
        success: function (res) {
            if (res.success && res.statusCode == 200) {
                window.location = "/Auth/Login";
            } else if (res.statusCode === 500) {
                formStepOne.reset();
                form.reset();
                errorP.text(res.error);
                errorP.css("display", "block");
                $("#step-two-form").removeClass("active");
                $("#step-one-form").addClass("active");
                otpBtn.prop("disabled", false);
                resetOtpTimer();
            } else if (res.errors) {
                Object.keys(res.errors).forEach(function (key) {
                    const fieldName = key.replace("StepTwo.", "");
                    const messages = res.errors[key];
                    const span = $(`span[data-valmsg-for='StepTwo.${fieldName}']`);
                    span.text(messages.join(", "));
                    span.css("color", "red");
                });
            } else {
                errorP.text(res.error);
                errorP.css("display", "block");
                otpBtn.prop("disabled", false);
            }
        },
        error: function (err) {
            resetOtpTimer();
        }
    });
});

function getOtpCods() {
    const otpBoxes = $(".otp-box");
    const otpFullInput = $("#otpCodFull");

    otpBoxes.on("input", function () {
        const current = $(this);
        let value = current.val();

        // 🔹 تبدیل عدد فارسی یا عربی به انگلیسی قبل از هر بررسی
        value = toEnglishDigits(value);
        current.val(value);

        // فقط عدد انگلیسی مجاز است
        if (!/^[0-9]$/.test(value)) {
            current.val("");
            return;
        }

        // اگر کاربر عدد وارد کرد، برو خانه بعد
        const nextBox = current.next(".otp-box");
        if (value && nextBox.length) {
            nextBox.focus();
        }

        // ساخت رشته کامل OTP
        let otpCode = "";
        otpBoxes.each(function () {
            otpCode += toEnglishDigits($(this).val());
        });

        // مقدار نهایی را در input مخفی قرار بده
        otpFullInput.val(otpCode);
    });

    // وقتی کاربر Backspace زد برگرد خانه قبل
    otpBoxes.on("keydown", function (e) {
        if (e.key === "Backspace" && !$(this).val()) {
            const prevBox = $(this).prev(".otp-box");
            if (prevBox.length) prevBox.focus();
        }
    });
}

function startTimer(duration) {
    if (otpCountdown) {
        clearInterval(otpCountdown);
        otpCountdown = null;
    }

    var endTime = Date.now() + duration * 1000;
    localStorage.setItem("otpEndTime", endTime.toString());

    updateTimerUI(duration);

    otpCountdown = setInterval(function () {
        var stored = localStorage.getItem("otpEndTime");
        if (!stored) {
            resetOtpTimer();
            return;
        }

        var remaining = Math.floor((parseInt(stored, 10) - Date.now()) / 1000);

        if (remaining <= 0) {
            clearInterval(otpCountdown);
            otpCountdown = null;

            $("#step-two-form").removeClass("active");
            $("#step-one-form").addClass("active");

            $("#error-register").text("زمان ارسال کد پایان یافت");
            $("#error-register").fadeIn();

            resetOtpTimer();
            return;
        }

        updateTimerUI(remaining);
    }, 1000);
}

function updateTimerUI(seconds) {
    var min = String(Math.floor(seconds / 60)).padStart(2, "0");
    var sec = String(seconds % 60).padStart(2, "0");
    $("#timer").text(min + ":" + sec);
}

function resetOtpTimer() {
    if (otpCountdown) {
        clearInterval(otpCountdown);
        otpCountdown = null;
    }
    localStorage.removeItem("otpEndTime");
    localStorage.removeItem("otpRequestId");

    $("#timer").text("00:00");
    $(".otp-box").val("");
    $("#otpCodFull").val("");
}

$(document).on("click", "#go-home", function (e) {
    window.location = "/";
});
