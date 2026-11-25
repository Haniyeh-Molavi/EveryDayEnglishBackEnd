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

    public async Task<List<WordDto>> GetAllAsync(string? search, Guid? categoryId)
    {
        var words = await _wordRepository.GetAllAsync(search, categoryId);

        return words.Select(WordDto.FromEntity).ToList();
    }

    public async Task<WordDto> GetByIdAsync(Guid id)
    {
        var word = await _wordRepository.GetByIdAsync(id);

        if (word == null)
            throw new KeyNotFoundException("Word not found.");

        return WordDto.FromEntity(word);
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

        // Ask Gemini for the translations, type, gender, category and example sentence
        var info = await _translationService.GetWordInfoAsync(text, request.Language, request.ExampleSentence?.Trim());

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
            EnglishExample = info.EnglishExample,
            PersianExample = info.PersianExample,
            PortugueseExample = info.PortugueseExample,
            CategoryId = category.Id,
            Category = category,
            CreatedAt = DateTime.UtcNow
        };

        await _wordRepository.AddAsync(word);

        return WordDto.FromEntity(word);
    }

    public async Task DeleteAsync(Guid id)
    {
        var word = await _wordRepository.GetByIdAsync(id);

        if (word == null)
            throw new KeyNotFoundException("Word not found.");

        // Also removes the word from every user's list (cascade delete)
        await _wordRepository.DeleteAsync(word);
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
