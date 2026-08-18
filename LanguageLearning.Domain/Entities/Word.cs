using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Domain.Entities;

public class Word
{
    public Guid Id { get; set; }

    public string Text { get; set; } = null!;

    public Language Language { get; set; }

    public WordType WordType { get; set; }

    public Gender Gender { get; set; }

    public Guid TranslationGroupId { get; set; }

    public TranslationGroup TranslationGroup { get; set; }
        = null!;
}