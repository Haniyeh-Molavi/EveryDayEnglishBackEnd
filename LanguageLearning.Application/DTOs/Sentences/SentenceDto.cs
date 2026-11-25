using LanguageLearning.Domain.Entities;

namespace LanguageLearning.Application.DTOs.Sentences;

public class SentenceDto
{
    public Guid Id { get; set; }
    public string EnglishText { get; set; } = string.Empty;
    public string PersianText { get; set; } = string.Empty;
    public string PortugueseText { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    public static SentenceDto FromEntity(CommonSentence sentence)
    {
        return new SentenceDto
        {
            Id = sentence.Id,
            EnglishText = sentence.EnglishText,
            PersianText = sentence.PersianText,
            PortugueseText = sentence.PortugueseText,
            IsActive = sentence.IsActive
        };
    }
}
