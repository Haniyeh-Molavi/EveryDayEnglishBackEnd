using LanguageLearning.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearning.Application.Interfaces
{
    public interface IWordMetadataService
    {
        Task<WordMetadataDto> AnalyzeAsync(
            string word,
            string languageCode);
    }
}
