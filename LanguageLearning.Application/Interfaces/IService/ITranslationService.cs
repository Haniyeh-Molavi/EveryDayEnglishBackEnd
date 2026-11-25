using LanguageLearning.Application.DTOs.Words;
using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Application.Interfaces.IService;

// Translates words between English, Portuguese and Persian (implemented with Gemini)
public interface ITranslationService
{
    Task<WordInfoDto> GetWordInfoAsync(string word, Language language);
}
