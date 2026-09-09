using LanguageLearning.Application.DTOs;
using LanguageLearning.Application.Interfaces;
using LanguageLearning.Domain.Enums;

namespace LanguageLearning.Application.Services;

public class WordMetadataService : IWordMetadataService
{
    public async Task<WordMetadataDto> AnalyzeAsync(string word, string languageCode)
    {
        return await Task.FromResult(new WordMetadataDto
        {
            WordType = WordType.Noun,   
            Gender = default,           
        });
    }
}