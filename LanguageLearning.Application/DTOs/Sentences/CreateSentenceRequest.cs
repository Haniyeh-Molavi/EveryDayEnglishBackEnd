using System.ComponentModel.DataAnnotations;
using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Application.DTOs.Sentences;

public class CreateSentenceRequest
{
    [Required, MaxLength(300)]
    public string Text { get; set; } = string.Empty;

    // The language of Text: 1 = English, 2 = Portuguese, 3 = Persian
    public Language Language { get; set; }
}
