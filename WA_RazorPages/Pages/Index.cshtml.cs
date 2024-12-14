using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WA_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
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
                Cnt = 3;
                Len = 3;
                MaxLen = 20;
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
            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5157/words/{{cnt, minLen, maxLen}}?cnt={cnt}&minLen={minLen}&maxLen={maxLen}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Words = JsonConvert.DeserializeObject<List<string>>(apiResponse).Select(x => x.ToUpper()).ToList();
                }
            }
        }












        public async void OnPostCallAPI()
        {
            await GetWords(2, 2, 2);

            /*string Baseurl = "https://localhost:44309/WeatherForecast";

            try
            {
                using (var client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri(Baseurl);
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("SecureApiKey", "12345");
                    HttpResponseMessage response = await client.SendAsync(request);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var statusCode = response.StatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        //API call success, Do your action
                    }

                    else
                    {
                        //API Call Failed, Check Error Details
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }*/
        }


        public async Task<JsonResult> OnGetWordsAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5157/words/{cnt, minLen}?cnt=2&minLen=2"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Words = JsonConvert.DeserializeObject<List<string>>(apiResponse);
                }
            }
            return new JsonResult(Words);
        }
    }
}
