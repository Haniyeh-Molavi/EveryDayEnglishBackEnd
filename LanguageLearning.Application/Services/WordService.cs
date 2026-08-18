using LanguageLearning.Application.Interfaces;
using LanguageLearning.Domain.Entities;

namespace LanguageLearning.Application.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository;

    public WordService(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public async Task CreateAsync(Word word)
    {
        await _wordRepository.AddAsync(word);
    }

    public Task CreateAsync(WordDetailsDto request)
    {
        throw new NotImplementedException();
    }
}