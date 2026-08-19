using LanguageLearning.Application.Interfaces;
using LanguageLearning.Domain.Entities;
using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Application.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository;
    private readonly TranslatorService _translator;

    public WordService(IWordRepository wordRepository, TranslatorService translator)
    {
        _wordRepository = wordRepository;
        _translator = translator;
    }

    public async Task CreateAsync(TranslationDto translationDto)
    {
        List<string> targetLanguages = ["en", "es", "fr"];

        switch (translationDto.LanguageCode)
        {
            case "en":
                targetLanguages.Remove("en");
                var result_en = await _translator.TranslateAsync(translationDto.Word, targetLanguages);
                break;
            case "es":
                targetLanguages.Remove("es");
                var result_es = await _translator.TranslateAsync(translationDto.Word, targetLanguages);
                break;
            case "fr":
                targetLanguages.Remove("fr");
                var result_fr = await _translator.TranslateAsync(translationDto.Word, targetLanguages);
                break;
            default:
                targetLanguages.Remove("en");
                var result = await _translator.TranslateAsync(translationDto.Word, targetLanguages);
                break;
        }

            var word = new Word
        {
            Id = Guid.NewGuid(),
            Text = translationDto.Word,
            Language = Enum.Parse<Language>(translationDto.LanguageCode),
            //TODO: Implement the WordType
            WordType = Enum.Parse<WordType>("Noun"),
            //TODO: Implement the Gender
            Gender = Enum.Parse<Gender>(translationDto.Gender ?? "Neutral"),
            //TODO: Implement the Category
            Category = new Category { Name = translationDto.Category }
        };
        await _wordRepository.AddAsync(word);
    }
}