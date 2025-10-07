using SaharBeautyWeb.Pages.Shared;

namespace SaharBeautyWeb.Pages
{
    public class IndexModel : LandingBasePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
