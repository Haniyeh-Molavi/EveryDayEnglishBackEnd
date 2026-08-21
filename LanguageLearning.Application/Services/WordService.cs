using LanguageLearning.Application.Interfaces;
using LanguageLearning.Application.Mappings;
using LanguageLearning.Domain.Entities;
using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Application.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository;
    private readonly TranslatorService _translator;
    private readonly IWordMetadataService _wordMetadataService;

    public WordService(
        IWordRepository wordRepository,
        TranslatorService translator,
        IWordMetadataService wordMetadataService)
    {
        _wordRepository = wordRepository;
        _translator = translator;
        _wordMetadataService = wordMetadataService;
    }

    public async Task CreateAsync(TranslationDto dto)
    {
        var translationGroup = new TranslationGroup
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        // 1. Always get English first
        var englishWord =
            dto.LanguageCode.ToLower() == "en"
                ? dto.Word
                : await _translator.TranslateAsync(
                    dto.Word,
                    dto.LanguageCode,
                    "en");

        // 2. Get Persian
        var persianWord =
            dto.LanguageCode.ToLower() == "fa"
                ? dto.Word
                : await _translator.TranslateAsync(
                    englishWord,
                    "en",
                    "fa");

        // 3. Get Portuguese
        var portugueseWord =
            dto.LanguageCode.ToLower() == "pt"
                ? dto.Word
                : await _translator.TranslateAsync(
                    englishWord,
                    "en",
                    "pt");

        // 4. Analyze Portuguese word
        var metadata =
            await _wordMetadataService.AnalyzeAsync(
                portugueseWord,
                "pt");

        var category = new Category
        {
            Name = dto.Category
        };

        // English
        translationGroup.Words.Add(new Word
        {
            Id = Guid.NewGuid(),
            Text = englishWord,
            Language = Language.English,
            WordType = metadata.WordType,
            Gender = Gender.None,
            Category = category
        });

        // Persian
        translationGroup.Words.Add(new Word
        {
            Id = Guid.NewGuid(),
            Text = persianWord,
            Language = Language.Persian,
            WordType = metadata.WordType,
            Gender = Gender.None,
            Category = category
        });

        // Portuguese
        translationGroup.Words.Add(new Word
        {
            Id = Guid.NewGuid(),
            Text = portugueseWord,
            Language = Language.Portuguese,
            WordType = metadata.WordType,
            Gender = metadata.Gender,
            Category = category
        });

        await _wordRepository.AddTranslationGroupAsync(
            translationGroup);
    }
}
