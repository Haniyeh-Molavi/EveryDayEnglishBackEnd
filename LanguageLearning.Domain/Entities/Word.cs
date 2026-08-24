using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Domain.Entities
{
public class Word
{
    public Guid Id { get; set; }

    public string EnglishWord { get; set; }

    public string PersianWord { get; set; }

    public string PortugueseWord { get; set; }

    public WordType Type { get; set; }

    public Gender PortugueseGender { get; set; }

    public string EnglishAudioUrl { get; set; }

    public string PersianAudioUrl { get; set; }

    public string PortugueseAudioUrl { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; }

    public DateTime CreatedAt { get; set; }
}
}