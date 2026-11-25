using LanguageLearning.Domain.Entities;

namespace LanguageLearning.Application.Interfaces.IRepository;

public interface IUserWordRepository
{
    // Returns the user's saved words (with Word and Category loaded)
    Task<List<UserWord>> GetByUserAsync(Guid userId, bool? isLearned);

    // Returns one saved word (with Word and Category loaded), or null
    Task<UserWord?> GetAsync(Guid userId, Guid wordId);

    Task AddAsync(UserWord userWord);

    Task UpdateAsync(UserWord userWord);

    Task DeleteAsync(UserWord userWord);
}
