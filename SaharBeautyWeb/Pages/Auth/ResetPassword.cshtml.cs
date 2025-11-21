using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Auth;
using SaharBeautyWeb.Models.Entities.Auth.Dtos;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.Auth;

namespace SaharBeautyWeb.Pages.Auth;

public class ResetPasswordModel : AjaxBasePageModel
{
    private readonly IAuthService _service;

    public ResetPasswordModel(
        ErrorMessages errorMessage,
        IAuthService service) : base(errorMessage)
    {
        _service = service;
    }

    [BindProperty]
    public RegisterStepOneModel StepOne { get; set; } = default!;

    [BindProperty]
    public ResetPasswordStepTwoModel StepTwo { get; set; } = default!;

    public void OnGet()
    {
    }


    public async Task<IActionResult> OnPostSendOtp()
    {
        if (StepOne.MobileNumber == null)
        {
            return HandleApiAjxResult(new ApiResultDto<String>()
            {
                Data = "شماره موبایل نامعتبر ",
                Error = "شماره موبایل نامعتبر",
                IsSuccess = false,
                StatusCode = 404

            });
        }

        var result = await _service.SendOtpResetPassword(StepOne.MobileNumber);
        if (result.IsSuccess && result.Data != null)
        {
            if (result.Data.VerifyStatusCode != 1)
            {
                result.StatusCode = 500;
                result.IsSuccess = false;
                result.Error = result.Data.VerifyStatus;
            }
        }
        var response = HandleApiAjxResult(result);
        return response;
    }


    public async Task<IActionResult> OnPostVerifyOtp()
    {
        var stepTwoErrors = ModelState
              .Where(x => x.Key.StartsWith("StepTwo.") &&
               x.Value?.Errors.Count > 0)
              .ToDictionary(
                  kvp => kvp.Key,
                  kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

        if (stepTwoErrors.Any())
        {
            return new JsonResult(new
            {
                success = false,
                errors = stepTwoErrors
            });
        }

        var result = await _service.VerifyOtpResetPassword(new VerifyOtpResetPasswordDto()
        {
            OtpCode = StepTwo.OtpCode,
            OtpRequestId = StepTwo.OtpRequestId,
            NewPassword = StepTwo.Password,
        });

        // در صورت موفقیت
        if (result.IsSuccess)
        {
            return new JsonResult(new
            {
                success = true,
                StatusCode = result.StatusCode
            });
        }
        else
        {
            return new JsonResult(new
            {
                StatusCode = result.StatusCode,
                Error = result.Error
            });
        }
    }
}
