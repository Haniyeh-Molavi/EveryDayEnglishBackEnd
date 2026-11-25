using LanguageLearning.Domain.Entities;

namespace LanguageLearning.Application.Interfaces.IRepository;

public interface ICategoryRepository
{
    Task<Category?> GetByNameAsync(string name);

    Task AddAsync(Category category);
}
