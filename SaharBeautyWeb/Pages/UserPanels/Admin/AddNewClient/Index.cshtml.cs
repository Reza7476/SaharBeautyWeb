using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Models.Entities.Clients.Dtos;
using SaharBeautyWeb.Models.Entities.Clients.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Clients.ClientServices;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.AddNewClient;

public class IndexModel : AjaxBasePageModel
{

    private readonly IClientService _clientService;
    public IndexModel(
        ErrorMessages errorMessage,
        IClientService clientService) : base(errorMessage)
    {
        _clientService = clientService;
    }

    [BindProperty]
    public AddNewClientModel NewClient { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }

    public void OnGet()
    {
        ViewData["ReturnUrl"] = ReturnUrl;
    }

    public async Task<IActionResult> OnPostRejecterClient()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _clientService.AddNewClient(new AddNewClientDto()
        {
            LastName = NewClient.LastName,
            Mobile = NewClient.Mobile,
            Name = NewClient.Name
        });

        var response = HandleApiResult(result);
        if (response is PageResult)
        {
            if (!string.IsNullOrWhiteSpace(ReturnUrl))
            {
                return LocalRedirect(ReturnUrl);
            }
            else
            {
                return RedirectToPage("/UserPanels/Admin/Index");
            }
        }
        return response;
    }
}
