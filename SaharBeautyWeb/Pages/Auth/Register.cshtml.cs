using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Models.Entities.Auth;
using SaharBeautyWeb.Models.Entities.Auth.Dtos;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.Auth;

namespace SaharBeautyWeb.Pages.Auth;

public class RegisterModel : AjaxBasePageModel
{

    private readonly IAuthService _service;


    public RegisterModel(
        ErrorMessages errorMessage,
        IAuthService service) : base(errorMessage)
    {
        _service = service;
    }

    [BindProperty]
    public RegisterStepOneModel StepOne { get; set; } = default!;
    [BindProperty]
    public RegisterStepTwoModel StepTwo { get; set; } = default!;

    public string? ErrorMessage { get; set; }

    public void OnGet()
    {
    }


    public async Task<IActionResult> OnPostSendOtp()
    {
        var stepOneError = ModelState
            .Where(x => x.Key.StartsWith("StepOne.") &&
                   x.Value?.Errors.Count > 0)
            .ToDictionary(kvp => kvp.Key,
             kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

        if (stepOneError.Any())
        {
            return new JsonResult(new
            {
                success = false,
                errors = stepOneError
            });
        }

        var result = await _service.SendOtp(StepOne.MobileNumber);
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

        var result = await _service.VerifyOtp(new VerifyOtpDto()
        {
            Email = StepTwo.Email,
            LastName = StepTwo.LastName,
            Name = StepTwo.Name,
            OtpCode = StepTwo.OtpCode,
            OtpRequestId = StepTwo.OtpRequestId,
            Password = StepTwo.Password,
            UserName = StepTwo.UserName,
        });

        // در صورت موفقیت
        if (result.IsSuccess && result.Data != null)
        {
            HttpContext.Session.Remove("JwtToken");
            HttpContext.Session.Remove("RefreshToken");
            HttpContext.Session.SetString(
                "JwtToken",
                result.Data.JwtToken != null ?
                result.Data.JwtToken : " ");
            HttpContext.Session.SetString(
                "RefreshToken",
                result.Data.RefreshToken != null ?
                result.Data.RefreshToken : " ");

            return new JsonResult(new
            {
                success = true,
                StatusCode = 200
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
