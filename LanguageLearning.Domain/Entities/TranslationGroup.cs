using LanguageLearning.Domain.Entities;

public class TranslationGroup
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<Word> Words { get; set; }
        = new List<Word>();
}