using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
public class TranslatorService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public TranslatorService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> TranslateAsync(string text, string targetLanguages)
    {
        var key = _configuration["AzureTranslator:Key"];
        var endpoint = _configuration["AzureTranslator:Endpoint"];
        var region = _configuration["AzureTranslator:Region"];

        var route = $"translate?api-version=3.0&to={targetLanguages}";
        var body = new[] { new { Text = text } };

        var request = new HttpRequestMessage(
            HttpMethod.Post,
            endpoint + route);

        request.Headers.Add("Ocp-Apim-Subscription-Key", key);
        request.Headers.Add("Ocp-Apim-Subscription-Region", region);

        request.Content = new StringContent(
            JsonSerializer.Serialize(body),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}