using System.Text;
using System.Text.Json;

namespace User.Services
{
    public class GroqService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GroqService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY")
              ?? throw new InvalidOperationException("Thiếu biến môi trường GROQ_API_KEY.");
        }

        public async Task<string> AskAsync(string prompt)
        {
            var url = "https://api.groq.com/openai/v1/chat/completions";

            var requestBody = new
            {
                model = "meta-llama/llama-4-scout-17b-16e-instruct", // Bạn cũng có thể thử "llama3-70b-8192"
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = 0.7
            };

            var requestJson = JsonSerializer.Serialize(requestBody);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _httpClient.PostAsync(url, requestContent);
            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    using var errorDoc = JsonDocument.Parse(responseText);
                    if (errorDoc.RootElement.TryGetProperty("error", out var errorElement) &&
                        errorElement.TryGetProperty("message", out var errorMessage))
                    {
                        return $"Lỗi API: {errorMessage.GetString()}";
                    }
                }
                catch
                {
                    return $"Lỗi API: {response.StatusCode}";
                }
            }

            using var doc = JsonDocument.Parse(responseText);
            if (doc.RootElement.TryGetProperty("choices", out var choices) &&
                choices.GetArrayLength() > 0 &&
                choices[0].TryGetProperty("message", out var message) &&
                message.TryGetProperty("content", out var content))
            {
                return content.GetString()?.Trim() ?? "";
            }

            return "Lỗi: Dữ liệu trả về không đúng định dạng mong đợi.";
        }
    }
}
