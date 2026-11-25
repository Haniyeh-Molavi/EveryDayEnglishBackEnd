using LanguageLearning.Domain.Entities;

namespace LanguageLearning.Application.Interfaces.IRepository;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();

    Task<Category?> GetByNameAsync(string name);

    Task AddAsync(Category category);
}
