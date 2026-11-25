using System.ComponentModel.DataAnnotations;

namespace LanguageLearning.Application.DTOs.Sentences;

// Used to fix a translation by hand, or to hide a sentence (IsActive = false)
public class UpdateSentenceRequest
{
    [Required, MaxLength(300)]
    public string EnglishText { get; set; } = string.Empty;

    [Required, MaxLength(300)]
    public string PersianText { get; set; } = string.Empty;

    [Required, MaxLength(300)]
    public string PortugueseText { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}
