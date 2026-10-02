using LanguageLearning.Application.Interfaces.IRepository;
using LanguageLearning.Application.Interfaces.IService;
using LanguageLearning.Application.Mappings;
using LanguageLearning.Domain.Entities;
using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Application.Services;

public class WordService : IWordService
{
    private readonly IWordInfoService _wordInfoService;
    private readonly IWordRepository _wordRepository;
    private readonly ICategoryRepository _categoryRepository;

    public WordService(
        IWordInfoService wordInfoService,
        IWordRepository wordRepository,
        ICategoryRepository categoryRepository
    )
    {
        _wordInfoService = wordInfoService;
        _wordRepository = wordRepository;
        _categoryRepository = categoryRepository;
    }

    private async Task<Category> GetOrCreateCategoryAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category is required.");

        name = name.Trim();

        var category = await _categoryRepository.GetCategoryByNameAsync(name);

        if (category == null)
        {
            category = new Category
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            await _wordRepository.AddCategoryAsync(category);
        }

        return category;
    }

    public async Task CreateAsync(TranslationDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        var info = await _wordInfoService.GetWordInfoAsync(dto.Word, dto.LanguageCode);

        WordType? type = dto.WordType != 0 ? dto.WordType : info.Type;

        if (type == null)
            throw new ArgumentException("Word type is required and could not be detected.");

        var gender = dto.Gender ?? info.PortugueseGender;

        var categoryName = string.IsNullOrWhiteSpace(dto.Category)
            ? info.SuggestedCategory
            : dto.Category;

        var category = await GetOrCreateCategoryAsync(categoryName ?? "");

        var word = new Word
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            EnglishWord = info.English,
            PersianWord = info.Persian,
            PortugueseWord = info.Portuguese,
            Type = type ?? WordType.None ,
            PortugueseGender = gender,
            Category = category,
            CategoryId = category.Id
        };

        var group = new TranslationGroup
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };
        group.Words.Add(word);

        await _wordRepository.AddTranslationGroupAsync(group);
    }
}
