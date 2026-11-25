using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Application.DTOs.Words;

// What the translation service (Gemini) tells us about a word
public class WordInfoDto
{
    public string English { get; set; } = string.Empty;
    public string Persian { get; set; } = string.Empty;
    public string Portuguese { get; set; } = string.Empty;

    // WordType.None means Gemini could not detect it
    public WordType Type { get; set; }
    public Gender PortugueseGender { get; set; }
    public string Category { get; set; } = string.Empty;

    public string EnglishExample { get; set; } = string.Empty;
    public string PersianExample { get; set; } = string.Empty;
    public string PortugueseExample { get; set; } = string.Empty;
}
