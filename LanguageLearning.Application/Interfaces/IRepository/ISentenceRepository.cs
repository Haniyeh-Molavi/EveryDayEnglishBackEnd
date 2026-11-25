using LanguageLearning.Domain.Entities;

namespace LanguageLearning.Application.Interfaces.IRepository;

public interface ISentenceRepository
{
    Task<List<CommonSentence>> GetAllAsync();

    Task<CommonSentence?> GetByIdAsync(Guid id);

    Task AddAsync(CommonSentence sentence);

    Task UpdateAsync(CommonSentence sentence);

    Task DeleteAsync(CommonSentence sentence);
}
