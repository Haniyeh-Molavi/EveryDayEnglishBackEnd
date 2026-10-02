using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearning.Application.Interfaces.IService
{
    public interface IWordInfoService
    {
        Task<WordInfoDto> GetWordInfoAsync(string word, string languageCode);
    }
}
