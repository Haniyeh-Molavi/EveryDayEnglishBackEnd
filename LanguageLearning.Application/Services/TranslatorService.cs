using System.Text;
using System.Text.Json;
using LanguageLearning.Application.Interfaces;
using LanguageLearning.Infrastructure.ExternalServices.AzureTranslator.Models;
using Microsoft.Extensions.Configuration;

namespace LanguageLearning.Infrastructure.ExternalServices.AzureTranslator;

public class TranslatorService : ITranslatorService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public TranslatorService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> TranslateAsync(
        string text,
        string fromLanguage,
        string toLanguage)
    {
        var key = _configuration["AzureTranslator:Key"];
        var endpoint = _configuration["AzureTranslator:Endpoint"];
        var region = _configuration["AzureTranslator:Region"];

        if (string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException("Azure Translator Key is missing.");

        if (string.IsNullOrWhiteSpace(endpoint))
            throw new InvalidOperationException("Azure Translator Endpoint is missing.");

        if (string.IsNullOrWhiteSpace(region))
            throw new InvalidOperationException("Azure Translator Region is missing.");

        var route =
            $"translate?api-version=3.0&from={fromLanguage}&to={toLanguage}";

        var body = new[]
        {
            new
            {
                Text = text
            }
        };

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{endpoint}{route}");

        request.Headers.Add(
            "Ocp-Apim-Subscription-Key",
            key);

        request.Headers.Add(
            "Ocp-Apim-Subscription-Region",
            region);

        request.Content = new StringContent(
            JsonSerializer.Serialize(body),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var json =
            await response.Content.ReadAsStringAsync();

        var result =
            JsonSerializer.Deserialize<List<TranslationResponse>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        return result?
            .FirstOrDefault()?
            .Translations?
            .FirstOrDefault()?
            .Text
            ?? throw new InvalidOperationException(
                $"No translation returned for '{text}'.");
    }
}