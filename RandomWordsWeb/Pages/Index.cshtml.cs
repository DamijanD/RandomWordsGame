using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace RandomWordsWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            Words = new List<string>();
        }

        public List<string> Words { get; set; }
        [BindProperty]
        public int Cnt { get; set; }
        [BindProperty]
        public int Len { get; set; }
        [BindProperty]
        public int MaxLen { get; set; }

        [BindProperty]
        public double Speed { get; set; }

        public void OnGet()
        {
            if (Cnt == 0)
            {
                Cnt = 10;
                Len = 3;
                MaxLen = 12;
                Speed = 1.5;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await GetWords(Cnt, Len, MaxLen);

            return Page();
        }

        private async Task GetWords(int cnt, int minLen, int maxLen)
        {
            var host = _configuration["WordsWebServiceHost"]; 
            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync($"{host}/words/{{cnt, minLen, maxLen}}?cnt={cnt}&minLen={minLen}&maxLen={maxLen}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Words = JsonConvert.DeserializeObject<List<string>>(apiResponse).Select(x => x.ToUpper()).ToList();
                }
            }
        }


    }
}
