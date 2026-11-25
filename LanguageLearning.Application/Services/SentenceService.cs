using LanguageLearning.Application.DTOs.Sentences;
using LanguageLearning.Application.Interfaces.IRepository;
using LanguageLearning.Application.Interfaces.IService;
using LanguageLearning.Domain.Entities;

namespace LanguageLearning.Application.Services;

public class SentenceService : ISentenceService
{
    private readonly ISentenceRepository _sentenceRepository;
    private readonly ITranslationService _translationService;

    public SentenceService(
        ISentenceRepository sentenceRepository,
        ITranslationService translationService)
    {
        _sentenceRepository = sentenceRepository;
        _translationService = translationService;
    }

    public async Task<List<SentenceDto>> GetAllAsync()
    {
        var sentences = await _sentenceRepository.GetAllAsync();

        return sentences.Select(SentenceDto.FromEntity).ToList();
    }

    public async Task<SentenceDto> GetByIdAsync(Guid id)
    {
        var sentence = await GetSentenceAsync(id);

        return SentenceDto.FromEntity(sentence);
    }

    public async Task<SentenceDto> CreateAsync(CreateSentenceRequest request)
    {
        var text = request.Text.Trim();

        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Text is required.");

        if (!Enum.IsDefined(request.Language))
            throw new ArgumentException("Language must be 1 (English), 2 (Portuguese) or 3 (Persian).");

        // Ask Gemini to fill in the other two languages
        var translation = await _translationService.TranslateSentenceAsync(text, request.Language);

        var sentence = new CommonSentence
        {
            Id = Guid.NewGuid(),
            EnglishText = translation.English,
            PersianText = translation.Persian,
            PortugueseText = translation.Portuguese,
            IsActive = true
        };

        await _sentenceRepository.AddAsync(sentence);

        return SentenceDto.FromEntity(sentence);
    }

    public async Task<SentenceDto> UpdateAsync(Guid id, UpdateSentenceRequest request)
    {
        var sentence = await GetSentenceAsync(id);

        sentence.EnglishText = request.EnglishText.Trim();
        sentence.PersianText = request.PersianText.Trim();
        sentence.PortugueseText = request.PortugueseText.Trim();
        sentence.IsActive = request.IsActive;

        await _sentenceRepository.UpdateAsync(sentence);

        return SentenceDto.FromEntity(sentence);
    }

    public async Task DeleteAsync(Guid id)
    {
        var sentence = await GetSentenceAsync(id);

        await _sentenceRepository.DeleteAsync(sentence);
    }

    private async Task<CommonSentence> GetSentenceAsync(Guid id)
    {
        var sentence = await _sentenceRepository.GetByIdAsync(id);

        if (sentence == null)
            throw new KeyNotFoundException("Sentence not found.");

        return sentence;
    }
}
