using LanguageLearning.Application.DTOs.Categories;

namespace LanguageLearning.Application.Interfaces.IService;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
}
