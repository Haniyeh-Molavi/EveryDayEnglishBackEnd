using LanguageLearning.Domain.Entities;
using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Application.DTOs.Words;

// What the API returns for a word
public class WordDto
{
    public Guid Id { get; set; }
    public string EnglishWord { get; set; } = string.Empty;
    public string PersianWord { get; set; } = string.Empty;
    public string PortugueseWord { get; set; } = string.Empty;
    public WordType Type { get; set; }
    public Gender PortugueseGender { get; set; }
    public string EnglishExample { get; set; } = string.Empty;
    public string PersianExample { get; set; } = string.Empty;
    public string PortugueseExample { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // The Word must be loaded together with its Category
    public static WordDto FromEntity(Word word)
    {
        return new WordDto
        {
            Id = word.Id,
            EnglishWord = word.EnglishWord,
            PersianWord = word.PersianWord,
            PortugueseWord = word.PortugueseWord,
            Type = word.Type,
            PortugueseGender = word.PortugueseGender,
            EnglishExample = word.EnglishExample,
            PersianExample = word.PersianExample,
            PortugueseExample = word.PortugueseExample,
            CategoryId = word.CategoryId,
            CategoryName = word.Category.Name,
            CreatedAt = word.CreatedAt
        };
    }
}
