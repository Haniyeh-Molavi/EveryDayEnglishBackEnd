using LanguageLearning.Domain.Enums;
using Microsoft.AspNetCore.Http;

public class TranslationDto
{
    public string LanguageCode { get; set; } = string.Empty;

    public string Word { get; set; } = string.Empty;

    public WordType WordType { get; set; }

    public Gender? Gender { get; set; }

    public string Category { get; set; } = string.Empty;

    public string ExampleSentence { get; set; } = string.Empty;

    public IFormFile? PronunciationAudioUrl { get; set; }
}
