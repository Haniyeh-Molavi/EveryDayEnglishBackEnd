using LanguageLearning.Domain.Entities;

namespace LanguageLearning.Application.Interfaces.IRepository;

public interface IWordRepository
{
    Task AddTranslationGroupAsync(TranslationGroup group);
    Task AddCategoryAsync(Category category);
    Task SaveChangesAsync();
}