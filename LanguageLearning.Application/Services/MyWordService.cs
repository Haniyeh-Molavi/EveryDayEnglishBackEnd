using LanguageLearning.Application.DTOs.MyWords;
using LanguageLearning.Application.Interfaces.IRepository;
using LanguageLearning.Application.Interfaces.IService;
using LanguageLearning.Domain.Entities;

namespace LanguageLearning.Application.Services;

public class MyWordService : IMyWordService
{
    private readonly IUserWordRepository _userWordRepository;
    private readonly IWordRepository _wordRepository;

    public MyWordService(
        IUserWordRepository userWordRepository,
        IWordRepository wordRepository)
    {
        _userWordRepository = userWordRepository;
        _wordRepository = wordRepository;
    }

    public async Task<List<MyWordDto>> GetMyWordsAsync(Guid userId, bool? isLearned)
    {
        var userWords = await _userWordRepository.GetByUserAsync(userId, isLearned);

        return userWords.Select(MyWordDto.FromEntity).ToList();
    }

    public async Task<MyWordDto> AddAsync(Guid userId, Guid wordId)
    {
        var word = await _wordRepository.GetByIdAsync(wordId);

        if (word == null)
            throw new KeyNotFoundException("Word not found.");

        var existing = await _userWordRepository.GetAsync(userId, wordId);

        if (existing != null)
            throw new ArgumentException("This word is already in your list.");

        var userWord = new UserWord
        {
            UserId = userId,
            WordId = word.Id,
            Word = word,
            SavedAt = DateTime.UtcNow,
            IsLearned = false
        };

        await _userWordRepository.AddAsync(userWord);

        return MyWordDto.FromEntity(userWord);
    }

    public async Task<MyWordDto> UpdateProgressAsync(Guid userId, Guid wordId, bool isLearned)
    {
        var userWord = await GetUserWordAsync(userId, wordId);

        userWord.IsLearned = isLearned;
        userWord.LearnedAt = isLearned ? DateTime.UtcNow : null;

        await _userWordRepository.UpdateAsync(userWord);

        return MyWordDto.FromEntity(userWord);
    }

    public async Task RemoveAsync(Guid userId, Guid wordId)
    {
        var userWord = await GetUserWordAsync(userId, wordId);

        await _userWordRepository.DeleteAsync(userWord);
    }

    public async Task<ProgressDto> GetProgressAsync(Guid userId)
    {
        var userWords = await _userWordRepository.GetByUserAsync(userId, null);

        var total = userWords.Count;
        var learned = userWords.Count(uw => uw.IsLearned);

        return new ProgressDto
        {
            TotalWords = total,
            LearnedWords = learned,
            NotLearnedWords = total - learned
        };
    }

    private async Task<UserWord> GetUserWordAsync(Guid userId, Guid wordId)
    {
        var userWord = await _userWordRepository.GetAsync(userId, wordId);

        if (userWord == null)
            throw new KeyNotFoundException("This word is not in your list.");

        return userWord;
    }
}
