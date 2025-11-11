using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Commons.Models;
using SaharBeautyWeb.Models.Entities.Users.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Users;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.Users
{
    public class IndexModel : AjaxBasePageModel
    {

        private readonly IUserService _userService;
        public IndexModel(
            ErrorMessages errorMessage,
            IUserService userService) : base(errorMessage)
        {
            _userService = userService;
        }
        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public GetAllUsersModel AllUsersModel { get; set; } = new();


        public async Task<IActionResult> OnGet(int pageNumber = 0, int limit = 5)
        {
            int offset = pageNumber;
            var result = await _userService.GetAllUsers(offset, limit, Search);

            if (result.IsSuccess && result.Data != null)
            {

                AllUsersModel = new GetAllUsersModel()
                {
                    AllUsers = result.Data.Elements.Select(_ => new AllUsersModel()
                    {
                        AppointmentNumber = _.AppointmentNumber,
                        Avatar = _.Avatar != null ? new ImageDetailsModel()
                        {
                            Extension = _.Avatar.Extension,
                            UniqueName = _.Avatar.UniqueName,
                            ImageName = _.Avatar.ImageName,
                            Url = _.Avatar.Url
                        } : null,

                        CreatedAt = DateTimeExtension.ConvertDateOnlyToPersian(DateOnly.FromDateTime(_.CreatedAt)),
                        Email = _.Email,
                        LastName = _.LastName,
                        IsActive = StringExtension.ConvertActiveBoolToString(_.IsActive),
                        Mobile = _.Mobile,
                        Name = _.Name,
                        Roles = _.Roles,
                        UserName = _.UserName,
                    }).ToList(),
                    CurrentPage = pageNumber,
                    TotalElements = result.Data.TotalElements,
                    TotalPages = result.Data.TotalElements.ToTotalPage(limit)
                };
            }

            var response = HandleApiResult(result);
            return response;
        }
    }
}
