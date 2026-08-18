using LanguageLearning.Domain.Entities;

public interface IWordRepository
{
    Task AddAsync(Word word);
    Task<List<Word>> GetAllAsync();
}