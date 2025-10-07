using System.Text.Json;

public class ErrorMessages
{
    private readonly IDictionary<string, string> _errors;

    public ErrorMessages(IWebHostEnvironment env)
    {
        // مسیر فایل: wwwroot/config/exception.json
        var filePath = Path.Combine(env.WebRootPath, "config", "exception.json");

        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            _errors = JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                      ?? new Dictionary<string, string>();
        }
        else
        {
            _errors = new Dictionary<string, string>();
        }
    }

    public string GetMessage(string errorKey)
    {
        if (string.IsNullOrWhiteSpace(errorKey))
        {
            if (_errors.TryGetValue("UnknownError", out var unknownMsg))
                return unknownMsg;
            return "خطای ناشناخته";
        }

        if (_errors.TryGetValue(errorKey, out var message))
            return message;

        if (_errors.TryGetValue("UnknownError", out var unknownMsg2))
            return unknownMsg2;

        return "خطای ناشناخته";
    }
}
