namespace LanguageLearning.Domain.Entities;

public class CommonSentence
{
    public Guid Id { get; set; }

    public string EnglishText { get; set; } = string.Empty;

    public string PersianText { get; set; } = string.Empty;

    public string PortugueseText { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}
