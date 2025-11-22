var otpCountdown = null;

// ====== تابع کمکی: تبدیل اعداد فارسی و عربی به انگلیسی ======
function toEnglishDigits(str) {
    if (!str) return "";
    return str
        .replace(/[\u06F0-\u06F9]/g, d => String.fromCharCode(d.charCodeAt(0) - 1728))
        .replace(/[\u0660-\u0669]/g, d => String.fromCharCode(d.charCodeAt(0) - 1584));
}

// ====== مرحله ۱: ارسال شماره موبایل ======
$(document).on("click", "#step-one-btn", function (e) {
    e.preventDefault();
    var errorP = $("#error-register");
    var otpRequestId = $("#Otp-request-id")[0];
    var form = $("#step-one-form")[0];
    var formData = new FormData(form);
    errorP.hide();

    ajaxWithButtonLoading({
        button: "#step-one-btn",
        url: sendOtp,
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (res) {
            console.log(res);
            otpRequestId.value = res.data.otpRequestId;
            $("#step-one-form").removeClass("active");
            $("#step-two-form").addClass("active");

            resetOtpTimer();
            startTimer(120);
        }

    });
});


// ====== مرحله ۲: تأیید کد و تکمیل ثبت‌نام ======
$(document).on("click", "#verify-otp-btn", function (e) {
    e.preventDefault();
    const formStepOne = $("#step-one-form")[0];
    const form = $("#step-two-form")[0];
    const formData = new FormData(form);
    var errorP = $("#error-register");
    var otpBtn = $("#step-one-btn");

    $("#step-two-form span[asp-validation-for]").text("");

    // قبل از ارسال، اطمینان از تبدیل کامل اعداد فارسی در OTP
    var otpCode = $("#otpCodFull").val();
    formData.set("StepTwo.OtpCode", toEnglishDigits(otpCode));

    $.ajax({
        type: "POST",
        url: verifyOtp,
        data: formData,
        processData: false,
        contentType: false,
        success: function (res) {
            if (res.success && res.statusCode == 200) {
                window.location.href = "/UserPanels/Client";
            } else if (res.statusCode === 500) {
                formStepOne.reset();
                form.reset();
                errorP.text(res.error).fadeIn();
                $("#step-two-form").removeClass("active");
                $("#step-one-form").addClass("active");
                otpBtn.prop("disabled", false);
                resetOtpTimer();
            } else if (res.errors) {
                Object.keys(res.errors).forEach(function (key) {
                    const fieldName = key.replace("StepTwo.", "");
                    const messages = res.errors[key];
                    const span = $(`span[data-valmsg-for='StepTwo.${fieldName}']`);
                    span.text(messages.join(", ")).css("color", "red");
                });
            }
        },
        error: function () {
            resetOtpTimer();
        }
    });
});


// ====== بخش OTP ======
getOtpCods();

function getOtpCods() {
    const otpBoxes = $(".otp-box");
    const otpFullInput = $("#otpCodFull");

    otpBoxes.on("input", function () {
        const current = $(this);
        let value = current.val();

        // تبدیل به عدد انگلیسی و حذف کاراکتر غیرعددی
        value = toEnglishDigits(value).replace(/\D/g, '').slice(0, 1);
        current.val(value);

        // حرکت به فیلد بعدی در صورت ورود عدد
        if (value && current.next(".otp-box").length) {
            current.next(".otp-box").focus();
        }

        // ساخت رشته کامل OTP
        let otpCode = "";
        otpBoxes.each(function () {
            otpCode += $(this).val();
        });
        otpFullInput.val(otpCode);
    });

    // حرکت به فیلد قبلی با Backspace
    otpBoxes.on("keydown", function (e) {
        if (e.key === "Backspace" && !$(this).val()) {
            const prevBox = $(this).prev(".otp-box");
            if (prevBox.length) prevBox.focus();
        }
    });
}


// ====== تایمر OTP ======
function startTimer(duration) {
    if (otpCountdown) clearInterval(otpCountdown);

    var endTime = Date.now() + duration * 1000;
    localStorage.setItem("otpEndTime", endTime.toString());
    updateTimerUI(duration);

    otpCountdown = setInterval(function () {
        var remaining = Math.floor((endTime - Date.now()) / 1000);
        if (remaining <= 0) {
            clearInterval(otpCountdown);
            otpCountdown = null;

            $("#step-two-form").removeClass("active");
            $("#step-one-form").addClass("active");
            $("#error-register").text("زمان ارسال کد پایان یافت").fadeIn();
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
    if (otpCountdown) clearInterval(otpCountdown);
    otpCountdown = null;
    localStorage.removeItem("otpEndTime");
    localStorage.removeItem("otpRequestId");

    $("#timer").text("00:00");
    $(".otp-box").val("");
    $("#otpCodFull").val("");
}


// ====== دکمه بازگشت ======
$(document).on("click", "#go-home", function () {
    window.location = "/";
});
