using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearning.Application.DTOs
{
    public class TranslationResponse
    {
        public List<TranslationItem> Translations { get; set; } = [];
    }

    public class TranslationItem
    {
        public string Text { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
    }
}
