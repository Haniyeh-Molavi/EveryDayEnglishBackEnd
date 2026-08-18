using LanguageLearning.Application.Interfaces;
using LanguageLearning.Domain.Entities;
using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Application.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository;

    public WordService(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public async Task CreateAsync(TranslationDto request)
    {

        var word = new Word
        {
            Id = Guid.NewGuid(),
            Text = request.Word,
            Language = Enum.Parse<Language>(request.LanguageCode),
            WordType = Enum.Parse<WordType>("Noun"),
            Gender = Enum.Parse<Gender>(request.Gender ?? "Neutral"),
        };
        await _wordRepository.AddAsync(word);
    }
}