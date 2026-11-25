using LanguageLearning.Application.DTOs.Sentences;

namespace LanguageLearning.Application.Interfaces.IService;

public interface ISentenceService
{
    Task<List<SentenceDto>> GetAllAsync();
    Task<SentenceDto> GetByIdAsync(Guid id);
    Task<SentenceDto> CreateAsync(CreateSentenceRequest request);
    Task<SentenceDto> UpdateAsync(Guid id, UpdateSentenceRequest request);
    Task DeleteAsync(Guid id);
}
