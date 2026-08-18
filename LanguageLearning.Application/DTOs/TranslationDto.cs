using Microsoft.AspNetCore.Http;

public class TranslationDto
{
    public string LanguageCode { get; set; } = string.Empty;

    public string Word { get; set; } = string.Empty;
    public IFormFile? PronunciationAudioUrl { get; set; }

    public string ExampleSentence { get; set; } = string.Empty;

    public string? Gender { get; set; }
}