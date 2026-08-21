using LanguageLearning.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearning.Application.Mappings;

public static class LanguageMapping
{
    public static Language ToLanguage(string code)
    {
        return code.ToLower() switch
        {
            "en" => Language.English,
            "fa" => Language.Persian,
            "pt" => Language.Portuguese,
            _ => throw new ArgumentException($"Unsupported language: {code}")
        };
    }

    public static string ToCode(Language language)
    {
        return language switch
        {
            Language.English => "en",
            Language.Persian => "fa",
            Language.Portuguese => "pt",
            _ => throw new ArgumentException($"Unsupported language: {language}")
        };
    }
}
