using LanguageLearning.Application.DTOs.Categories;
using LanguageLearning.Application.Interfaces.IRepository;
using LanguageLearning.Application.Interfaces.IService;

namespace LanguageLearning.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return categories
            .Select(c => new CategoryDto { Id = c.Id, Name = c.Name })
            .ToList();
    }
}
