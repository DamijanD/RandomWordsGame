using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RandomWordsWeb.Pages
{
    public class InstructionsModel : PageModel
    {
        private readonly ILogger<InstructionsModel> _logger;

        public InstructionsModel(ILogger<InstructionsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}
