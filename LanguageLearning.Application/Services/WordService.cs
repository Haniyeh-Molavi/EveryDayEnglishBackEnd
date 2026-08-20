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

    private async Task<string> TranslateToEnglishAsync(TranslationDto dto)
    {
        var result_en = await _translator.TranslateAsync(dto.Word, "en");
        var translationGroup = new TranslationGroup
        {
            Words= new List<Word>
            {
                new Word
                {
                    Id = Guid.NewGuid(),
                    Text = result_en,
                    Language = Language.English,
                    WordType = Enum.Parse<WordType>("Noun"),
                    Gender = Enum.Parse<Gender>("None"),
                    Category = new Category { Name = dto.Category }
                },
                new Word
                {
                    Id = Guid.NewGuid(),
                    Text = dto.Word,
                    Language = Enum.Parse<Language>(dto.LanguageCode),
                    WordType = Enum.Parse<WordType>("Noun"),
                    Gender
            }
        };
        return await _translator.TranslateAsync(dto.Word, targetLanguages);
    }

    private async Task<string> TranslateToPersianAsync(TranslationDto dto)
    {
        List<string> targetLanguages = ["en", "pt", "fr"];

        targetLanguages.Remove(dto.LanguageCode);
        var result_en = await _translator.TranslateAsync(translationDto.Word, targetLanguages);
        return await _translator.TranslateAsync(dto.Word, targetLanguages);
    }

    private async Task<string> TranslateToPortugueseAsync(TranslationDto dto)
    {
        List<string> targetLanguages = ["en", "pt", "fr"];

        targetLanguages.Remove(dto.LanguageCode);
        var result_en = await _translator.TranslateAsync(translationDto.Word, targetLanguages);
        return await _translator.TranslateAsync(dto.Word, targetLanguages);
    }

    public async Task CreateAsync(TranslationDto translationDto)
    {
        var translations = await TranslateToEnglishAsync(translationDto);
        
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