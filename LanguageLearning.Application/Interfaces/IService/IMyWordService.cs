using LanguageLearning.Application.DTOs.MyWords;

namespace LanguageLearning.Application.Interfaces.IService;

// The logged-in user's personal word list and learning progress
public interface IMyWordService
{
    // isLearned: null = all words, true = only learned, false = only not learned
    Task<List<MyWordDto>> GetMyWordsAsync(Guid userId, bool? isLearned);
    Task<MyWordDto> AddAsync(Guid userId, Guid wordId);
    Task<MyWordDto> UpdateProgressAsync(Guid userId, Guid wordId, bool isLearned);
    Task RemoveAsync(Guid userId, Guid wordId);
    Task<ProgressDto> GetProgressAsync(Guid userId);
}
