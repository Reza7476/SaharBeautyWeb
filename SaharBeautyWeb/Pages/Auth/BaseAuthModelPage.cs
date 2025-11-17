using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace SaharBeautyWeb.Pages.Auth;

public class BaseAuthModelPage : PageModel
{
    private Dictionary<string, string>? _translations;

    public string TranslateError(string? backendError)
    {
        if (string.IsNullOrWhiteSpace(backendError))
            return "خطای ناشناخته";

        LoadErrorTranslations();

        if (_translations != null && _translations.TryGetValue(
            backendError,
            out string? translated))
        {
            return translated;
        }

        return backendError;
    }

    private void LoadErrorTranslations()
    {
        if (_translations != null) return;

        var filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot/Config/exception.json");

        if (System.IO.File.Exists(filePath))
        {
            var json = System.IO.File.ReadAllText(filePath);
            _translations = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        else
        {
            _translations = new Dictionary<string, string>();
        }
    }
}
