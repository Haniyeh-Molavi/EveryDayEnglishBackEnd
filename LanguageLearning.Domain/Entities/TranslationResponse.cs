namespace LanguageLearning.Infrastructure.ExternalServices.AzureTranslator.Models;

public class TranslationResponse
{
    public List<TranslationItem> Translations { get; set; } = [];
}

public class TranslationItem
{
    public string Text { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
}