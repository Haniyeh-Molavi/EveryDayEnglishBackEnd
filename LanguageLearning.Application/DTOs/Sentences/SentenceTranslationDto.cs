namespace LanguageLearning.Application.DTOs.Sentences;

// What the translation service (Gemini) returns for a sentence
public class SentenceTranslationDto
{
    public string English { get; set; } = string.Empty;
    public string Persian { get; set; } = string.Empty;
    public string Portuguese { get; set; } = string.Empty;
}
