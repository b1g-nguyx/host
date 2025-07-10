using Microsoft.AspNetCore.Mvc;

namespace User.Controllers
{
    public class ProxyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProxyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("/proxy")]
        public async Task<IActionResult> ProxyPage([FromQuery] string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return BadRequest("Missing URL");

            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode);

                var content = await response.Content.ReadAsStringAsync();

                // Xử lý base tag để ảnh, CSS, JS load đúng
                content = content.Replace("</head>", $"<base href=\"{url}\"></head>");

                Response.Headers.Remove("X-Frame-Options");
                Response.Headers.Remove("Content-Security-Policy");

                return Content(content, "text/html");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching content: {ex.Message}");
            }
        }
    }
}
