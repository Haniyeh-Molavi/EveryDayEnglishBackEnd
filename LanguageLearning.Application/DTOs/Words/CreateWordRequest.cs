using System.ComponentModel.DataAnnotations;
using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Application.DTOs.Words;

public class CreateWordRequest
{
    [Required, MaxLength(50)]
    public string Word { get; set; } = string.Empty;

    // The language of Word: 1 = English, 2 = Portuguese, 3 = Persian
    public Language Language { get; set; }

    // Optional. If you leave these empty, Gemini fills them in.
    public WordType? Type { get; set; }
    public Gender? PortugueseGender { get; set; }
    public string? Category { get; set; }

    // Optional, in the same language as Word. If empty, Gemini writes one.
    [MaxLength(300)]
    public string? ExampleSentence { get; set; }
}
