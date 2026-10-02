using LanguageLearning.Domain.Enums;
using Microsoft.AspNetCore.Http;

public record WordInfoDto(
    string English,
    string Persian,
    string Portuguese,
    WordType? Type,
    Gender PortugueseGender,
    string? SuggestedCategory);

