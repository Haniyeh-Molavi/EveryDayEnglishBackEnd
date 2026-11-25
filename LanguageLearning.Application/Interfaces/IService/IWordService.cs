using LanguageLearning.Application.DTOs.Words;

namespace LanguageLearning.Application.Interfaces.IService;

public interface IWordService
{
    Task<WordDto> CreateAsync(CreateWordRequest request);
}
