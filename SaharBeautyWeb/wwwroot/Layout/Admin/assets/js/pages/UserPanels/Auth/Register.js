
//var otpCountdown = null;
//$(document).on("click", "#step-one-btn", function (e) {
//    //e.preventDefault();
//    var errorP = $("#error-register");
//    var otpRequestId = $("#Otp-request-id")[0];
//    var form = $("#step-one-form")[0];
//    var formData = new FormData(form);
//    var otpBtn = $(this);
//    otpBtn.prop("disabled", true);
//    $.ajax({
//        url: sendOtp,
//        type: 'Post',
//        data: formData,
//        contentType: false,
//        processData: false,
//        success: function (res) {
//            if (res.success) {
//                if (res.data.verifyStatusCode === 1 ||
//                    res.data.verifyStatus === "عملیات موفق" ||
//                    res.data.otpRequestId.length > 11) {
//                    otpRequestId.value = res.data.otpRequestId;
//                    $("#step-one-form").removeClass("active");
//                    $("#step-two-form").addClass("active");

//                    resetOtpTimer();

//                    startTimer(120);
//                } else if (res.statusCode === 500) {
//                    errorP.text(res.error);
//                    errorP.css("display", "block");
//                    otpBtn.prop("disabled", false);
//                }
//            } else if (res.errors) {

//                Object.keys(res.errors).forEach(function (key) {
//                    const fieldName = key.replace("StepOne.", "");
//                    const messages = res.errors[key];
//                    const span = $(`span[data-valmsg-for='StepOne.${fieldName}']`);
//                    span.text(messages.join(", "));
//                    span.css("color", "red");
//                });
//                otpBtn.prop("disabled", false);
//                //location.reload();
//            }
//            else {
//                errorP.text(res.error);
//                errorP.css("display", "block");
//                otpBtn.prop("disabled", false);
//            }
//        },
//        error: function (err) {
//            errorP.text(err);
//            errorP.css("display", "block");
//            otpBtn.prop("disabled", false);
//            //  location.reload();
//        }
//    });
//})



//getOtpCods();

//$(document).on("click", "#verify-otp-btn", function (e) {
//    e.preventDefault();
//    const formStepOne = $("#step-one-form")[0]
//    const form = $("#step-two-form")[0];
//    const formData = new FormData(form);
//    var errorP = $("#error-register");
//    var otpBtn = $("#step-one-btn");
//    $("#step-two-form span[asp-validation-for]").text("");

//    $.ajax({
//        type: "POST",
//        url: verifyOtp,
//        data: formData,
//        processData: false,
//        contentType: false,
//        success: function (res) {
//            if (res.success && res.statusCode == 200) {
//                window.location = "/UserPanels";
//            } else if (res.statusCode === 500) {
//                formStepOne.reset();
//                form.reset();
//                errorP.text(res.error);
//                errorP.css("display", "block");
//                $("#step-two-form").removeClass("active");
//                $("#step-one-form").addClass("active");
//                otpBtn.prop("disabled", false);
//                resetOtpTimer();
//            } else if (res.errors) {
//                Object.keys(res.errors).forEach(function (key) {
//                    const fieldName = key.replace("StepTwo.", "");
//                    const messages = res.errors[key];
//                    const span = $(`span[data-valmsg-for='StepTwo.${fieldName}']`);
//                    span.text(messages.join(", "));
//                    span.css("color", "red");
//                });
//            }
//        },
//        error: function (err) {
//            resetOtpTimer();
//        }
//    });
//});

//function getOtpCods() {
//    const otpBoxes = $(".otp-box");
//    const otpFullInput = $("#otpCodFull");

//    otpBoxes.on("input", function () {
//        const current = $(this);
//        const value = current.val();

//        // فقط عدد مجاز است
//        if (!/^[0-9]$/.test(value)) {
//            current.val("");
//            return;
//        }

//        // اگر کاربر عدد وارد کرد، برو خانه بعد
//        const nextBox = current.next(".otp-box");
//        if (value && nextBox.length) {
//            nextBox.focus();
//        }

//        // ساخت رشته کامل OTP
//        let otpCode = "";
//        otpBoxes.each(function () {
//            otpCode += $(this).val();
//        });

//        // مقدار نهایی را در input مخفی قرار بده
//        otpFullInput.val(otpCode);
//    });

//    // وقتی کاربر Backspace زد برگرد خانه قبل
//    otpBoxes.on("keydown", function (e) {
//        if (e.key === "Backspace" && !$(this).val()) {
//            const prevBox = $(this).prev(".otp-box");
//            if (prevBox.length) prevBox.focus();
//        }
//    });

//}

//function startTimer(duration) {

//    if (otpCountdown) {
//        clearInterval(otpCountdown);
//        otpCountdown = null;
//    }

//    // محاسبه endTime و ذخیره در localStorage برای قابلیت resume یا بررسی
//    var endTime = Date.now() + duration * 1000;
//    localStorage.setItem("otpEndTime", endTime.toString());

//    // به‌روزرسانی فوری UI برای نمایش مقدار اولیه
//    updateTimerUI(duration);

//    // ساخته شدن interval و ذخیره در متغیر سراسری
//    otpCountdown = setInterval(function () {
//        var stored = localStorage.getItem("otpEndTime");
//        if (!stored) {
//            // اگر به هر دلیلی حذف شده، ریست کن
//            resetOtpTimer();
//            return;
//        }

//        var remaining = Math.floor((parseInt(stored, 10) - Date.now()) / 1000);

//        if (remaining <= 0) {
//            clearInterval(otpCountdown);
//            otpCountdown = null;

//            // عمل‌هایی که زمان تمام شد باید انجام بشه
//            $("#step-two-form").removeClass("active");
//            $("#step-one-form").addClass("active");

//            $("#error-register").text("زمان ارسال کد پایان یافت");
//            $("#error-register").fadeIn();

//            resetOtpTimer();
//            return;
//        }

//        updateTimerUI(remaining);
//    }, 1000);
//}
//function updateTimerUI(seconds) {
//    var min = String(Math.floor(seconds / 60)).padStart(2, "0");
//    var sec = String(seconds % 60).padStart(2, "0");
//    $("#timer").text(min + ":" + sec);
//}



//function resetOtpTimer() {
//    if (otpCountdown) {
//        clearInterval(otpCountdown);
//        otpCountdown = null;
//    }
//    localStorage.removeItem("otpEndTime");
//    localStorage.removeItem("otpRequestId");

//    $("#timer").text("00:00");
//    $(".otp-box").val("");
//    $("#otpCodFull").val("");
//}

//$(document).on("click", "#go-home", function (e) {
//    window.location = "/";
//})


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
    //e.preventDefault();
    var errorP = $("#error-register");
    var otpRequestId = $("#Otp-request-id")[0];
    var form = $("#step-one-form")[0];
    var formData = new FormData(form);
    var otpBtn = $(this);

    otpBtn.prop("disabled", true);
    errorP.hide();

    $.ajax({
        url: sendOtp,
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.success) {
                if (res.data.verifyStatusCode === 1 ||
                    res.data.verifyStatus === "عملیات موفق" ||
                    res.data.otpRequestId.length > 11) {

                    otpRequestId.value = res.data.otpRequestId;
                    $("#step-one-form").removeClass("active");
                    $("#step-two-form").addClass("active");

                    resetOtpTimer();
                    startTimer(120);
                } else if (res.statusCode === 500) {
                    errorP.text(res.error).fadeIn();
                    otpBtn.prop("disabled", false);
                }
            } else if (res.errors) {
                Object.keys(res.errors).forEach(function (key) {
                    const fieldName = key.replace("StepOne.", "");
                    const messages = res.errors[key];
                    const span = $(`span[data-valmsg-for='StepOne.${fieldName}']`);
                    span.text(messages.join(", ")).css("color", "red");
                });
                otpBtn.prop("disabled", false);
            } else {
                errorP.text(res.error).fadeIn();
                otpBtn.prop("disabled", false);
            }
        },
        error: function (err) {
            errorP.text("خطا در ارسال درخواست").fadeIn();
            otpBtn.prop("disabled", false);
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
                window.location = "/UserPanels";
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
