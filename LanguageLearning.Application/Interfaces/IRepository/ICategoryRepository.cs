using LanguageLearning.Domain.Entities;


namespace LanguageLearning.Application.Interfaces.IRepository
{
    public interface ICategoryRepository
    {
        Task AddTranslationGroupAsync(TranslationGroup group);
        Task<Category?> GetCategoryByNameAsync(string name);
        Task AddCategoryAsync(Category category);
    }
}
