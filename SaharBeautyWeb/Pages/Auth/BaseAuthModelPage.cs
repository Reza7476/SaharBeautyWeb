using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace SaharBeautyWeb.Pages.Auth;

public class BaseAuthModelPage:PageModel
{
    private static Dictionary<string, string>? _translations;
    private static readonly object _lock = new();
    private static void EnsureTranslationsLoaded()
    {
        if (_translations != null)
            return;

        lock (_lock)
        {
            if (_translations != null)
                return;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "errors.fa.json");

            if (!System.IO.File.Exists(filePath))
            {
                _translations = new Dictionary<string, string>();
                return;
            }

            var json = System.IO.File.ReadAllText(filePath);

            _translations = JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                            ?? new Dictionary<string, string>();
        }
    }
    protected string TranslateError(string? backendError)
    {
        EnsureTranslationsLoaded();

        if (string.IsNullOrWhiteSpace(backendError))
            return "خطای ناشناخته";

        string key = backendError.Trim();

        // اگر backendError یک JSON باشد:
        try
        {
            var obj = JsonSerializer.Deserialize<BackendErrorResponse>(backendError);

            if (!string.IsNullOrEmpty(obj?.Error))
                key = obj.Error!;
        }
        catch
        {
            // JSON نبود → اشکالی ندارد
        }

        // اگر ترجمه وجود دارد
        if (_translations!.TryGetValue(key, out var translated))
            return translated;

        // اگر نبود، خود key را برگردان
        return key;
    }
    public class BackendErrorResponse
    {
        public string? Error { get; set; }
        public string? Description { get; set; }
        public int StatusCode { get; set; }
    }
}
