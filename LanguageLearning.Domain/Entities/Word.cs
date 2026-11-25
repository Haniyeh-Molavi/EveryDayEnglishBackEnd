using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Domain.Entities;

// One word in all three languages
public class Word
{
    public Guid Id { get; set; }

    public string EnglishWord { get; set; } = string.Empty;

    public string PersianWord { get; set; } = string.Empty;

    public string PortugueseWord { get; set; } = string.Empty;

    public WordType Type { get; set; }

    public Gender PortugueseGender { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
