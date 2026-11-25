using LanguageLearning.Application.DTOs.Sentences;
using LanguageLearning.Application.DTOs.Words;
using LanguageLearning.Application.Interfaces.IService;
using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Tests.Helpers;

// Used instead of Gemini in tests: always returns the same answer, no internet needed
public class FakeTranslationService : ITranslationService
{
    public int CallCount { get; private set; }

    public WordInfoDto WordInfo { get; set; } = new WordInfoDto
    {
        English = "cat",
        Persian = "گربه",
        Portuguese = "gato",
        Type = WordType.Noun,
        PortugueseGender = Gender.Masculine,
        Category = "Animals",
        EnglishExample = "The cat is sleeping.",
        PersianExample = "گربه خوابیده است.",
        PortugueseExample = "O gato está a dormir."
    };

    public SentenceTranslationDto SentenceTranslation { get; set; } = new SentenceTranslationDto
    {
        English = "How are you?",
        Persian = "حالت چطور است؟",
        Portuguese = "Como estás?"
    };

    public Task<WordInfoDto> GetWordInfoAsync(string word, Language language, string? exampleSentence)
    {
        CallCount++;
        return Task.FromResult(WordInfo);
    }

    public Task<SentenceTranslationDto> TranslateSentenceAsync(string text, Language language)
    {
        CallCount++;
        return Task.FromResult(SentenceTranslation);
    }
}
