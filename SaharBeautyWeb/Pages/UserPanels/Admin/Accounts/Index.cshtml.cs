using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Commons.Models;
using SaharBeautyWeb.Models.Entities.Users.Dtos;
using SaharBeautyWeb.Models.Entities.Users.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Users;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.Accounts;

public class IndexModel : AjaxBasePageModel
{
    private readonly IUserService _userService;
    public IndexModel(ErrorMessages errorMessage,
        IUserService userService) : base(errorMessage)
    {
        _userService = userService;
    }
    public UserInfoModel UserInfo { get; set; }

    [BindProperty]
    public EditUserInfoModel EditUserInfoModel { get; set; }

    [BindProperty]
    public IFormFile? Avatar { get; set; }

    public async Task<IActionResult> OnGet()
    {

        var result = await _userService.GetUserInfo();
        var response = HandleApiResult(result);

        if (result.IsSuccess)
        {
            if (result.Data != null)
            {
                UserInfo = new UserInfoModel()
                {
                    Avatar = result.Data.Avatar != null ? new ImageDetailsModel()
                    {
                        Extension = result.Data.Avatar.Extension,
                        ImageName = result.Data.Avatar.ImageName,
                        UniqueName = result.Data.Avatar.UniqueName,
                        Url = result.Data.Avatar.Url
                    } : null,
                    BirthDate = result.Data.BirthDate != null ? result.Data.BirthDate.Value.ToShamsi() : " ",
                    CreationDate = result.Data.CreationDate.ToShamsi(),
                    Email = result.Data.Email,
                    Id = result.Data.Id,
                    IsActive = result.Data.IsActive,
                    LastName = result.Data.LastName,
                    Mobile = result.Data.Mobile,
                    Name = result.Data.Name,
                    RoleNames = result.Data.RoleNames,
                    UserName = result.Data.UserName
                };
            }
        }
        return response;

    }

    public async Task<IActionResult> OnGetUserInfoForEdit()
    {
        var result = await _userService.GetUserInfo();
        var response = HandleApiAjaxPartialResult(
            result,
            data => new EditUserInfoModel()
            {
                Avatar = data.Avatar != null ? new ImageDetailsModel()
                {
                    Extension = data.Avatar.Extension,
                    ImageName = data.Avatar.ImageName,
                    UniqueName = data.Avatar.UniqueName,
                    Url = data.Avatar.Url
                } : null,
                BirthDate = data.BirthDate != null ? data.BirthDate.Value.ToShamsi() : " ",
                CreationDate = data.CreationDate.ToShamsi(),
                Name = data.Name,
                LastName = data.LastName,
                Mobile = data.Mobile,
                Email = data.Email,
            }, "_editUserInfoPartial");
        return response;
    }

    public async Task<IActionResult> OnPostApplyEditProfile()
    {
        if (EditUserInfoModel.BirthDate != null)
        {
            EditUserInfoModel.BirthDate = EditUserInfoModel.BirthDate.ConvertPersianNumberToEnglish();
        }
        var result = await _userService.EditAdminProfile(new EditAdminProfileDto()
        {
            Email = EditUserInfoModel.Email,
            LastName = EditUserInfoModel.LastName,
            Name = EditUserInfoModel.Name,
            BirthDateGregorian = EditUserInfoModel.BirthDate != null ?
                                  EditUserInfoModel.BirthDate
                                  .ConvertStringShamsiCalendarToGregorian()
                                  : null
        });

        var response = HandleApiAjxResult(result);
        return response;
    }

    public async Task<IActionResult> OnPostApplyEditProfileImage()
    {
        var (isValid, message) = Avatar.ValidateImage();
        if (!isValid)
        {
            return new JsonResult(new
            {
                success = false,
                error = message
            });
        }

        var result = await _userService.EditProfileImage(new EditMediaDto()
        {
            Media=Avatar
        });

        var response = HandleApiAjxResult(result);
        return response;
    }

}
