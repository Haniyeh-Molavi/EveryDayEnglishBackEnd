using LanguageLearning.Application.DTOs.Sentences;
using LanguageLearning.Application.DTOs.Words;
using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Application.Interfaces.IService;

// Translates between English, Portuguese and Persian (implemented with Gemini)
public interface ITranslationService
{
    // exampleSentence is optional. If it is empty, the service writes one.
    Task<WordInfoDto> GetWordInfoAsync(string word, Language language, string? exampleSentence);

    Task<SentenceTranslationDto> TranslateSentenceAsync(string text, Language language);
}
