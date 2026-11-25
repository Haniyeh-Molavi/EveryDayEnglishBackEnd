using LanguageLearning.Domain.Entities;
using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Application.Interfaces.IRepository;

public interface IWordRepository
{
    // search and categoryId are optional filters
    Task<List<Word>> GetAllAsync(string? search, Guid? categoryId);

    Task<Word?> GetByIdAsync(Guid id);

    // True if a word with this text already exists in the given language
    Task<bool> ExistsAsync(string text, Language language);

    Task AddAsync(Word word);

    Task DeleteAsync(Word word);
}
