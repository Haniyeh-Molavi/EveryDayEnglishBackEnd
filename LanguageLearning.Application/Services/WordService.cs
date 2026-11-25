using LanguageLearning.Application.DTOs.Words;
using LanguageLearning.Application.Interfaces.IRepository;
using LanguageLearning.Application.Interfaces.IService;
using LanguageLearning.Domain.Entities;
using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Application.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITranslationService _translationService;

    public WordService(
        IWordRepository wordRepository,
        ICategoryRepository categoryRepository,
        ITranslationService translationService)
    {
        _wordRepository = wordRepository;
        _categoryRepository = categoryRepository;
        _translationService = translationService;
    }

    public async Task<WordDto> CreateAsync(CreateWordRequest request)
    {
        var text = request.Word.Trim();

        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Word is required.");

        if (!Enum.IsDefined(request.Language))
            throw new ArgumentException("Language must be 1 (English), 2 (Portuguese) or 3 (Persian).");

        if (request.Type != null && !Enum.IsDefined(request.Type.Value))
            throw new ArgumentException("Type is not valid.");

        if (request.PortugueseGender != null && !Enum.IsDefined(request.PortugueseGender.Value))
            throw new ArgumentException("PortugueseGender is not valid.");

        if (await _wordRepository.ExistsAsync(text, request.Language))
            throw new ArgumentException($"The word \"{text}\" already exists.");

        // Ask Gemini for the translations, type, gender and category
        var info = await _translationService.GetWordInfoAsync(text, request.Language);

        // Values the user sent win over the ones Gemini suggests
        var type = request.Type is null or WordType.None ? info.Type : request.Type.Value;
        if (type == WordType.None)
            throw new ArgumentException("Could not detect the word type. Please send it in the request.");

        var gender = request.PortugueseGender ?? info.PortugueseGender;

        var categoryName = string.IsNullOrWhiteSpace(request.Category)
            ? info.Category
            : request.Category;

        var category = await GetOrCreateCategoryAsync(categoryName);

        var word = new Word
        {
            Id = Guid.NewGuid(),
            EnglishWord = info.English,
            PersianWord = info.Persian,
            PortugueseWord = info.Portuguese,
            Type = type,
            PortugueseGender = gender,
            CategoryId = category.Id,
            Category = category,
            CreatedAt = DateTime.UtcNow
        };

        await _wordRepository.AddAsync(word);

        return WordDto.FromEntity(word);
    }

    private async Task<Category> GetOrCreateCategoryAsync(string name)
    {
        name = name.Trim();

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category is required.");

        var category = await _categoryRepository.GetByNameAsync(name);

        if (category == null)
        {
            category = new Category
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            await _categoryRepository.AddAsync(category);
        }

        return category;
    }
}
