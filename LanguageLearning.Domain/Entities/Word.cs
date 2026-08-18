using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Domain.Entities;

public class Word
{
    public Guid Id { get; set; }

    public string Text { get; set; } = string.Empty;

    public string Pronunciation { get; set; } = string.Empty;

    public Language Language { get; set; }

    public PartOfSpeech PartOfSpeech { get; set; }

    public Gender Gender { get; set; } = Gender.None;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}