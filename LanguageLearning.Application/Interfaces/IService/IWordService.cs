using LanguageLearning.Application.DTOs.Words;

namespace LanguageLearning.Application.Interfaces.IService;

public interface IWordService
{
    Task<List<WordDto>> GetAllAsync(string? search, Guid? categoryId);
    Task<WordDto> GetByIdAsync(Guid id);
    Task<WordDto> CreateAsync(CreateWordRequest request);
    Task DeleteAsync(Guid id);
}
