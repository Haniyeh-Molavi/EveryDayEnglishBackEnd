using LanguageLearning.Domain.Entities;

namespace LanguageLearning.Application.Interfaces;

public interface IWordRepository
{
    Task<List<Word>> GetAllAsync();

    Task AddAsync(Word word);
}