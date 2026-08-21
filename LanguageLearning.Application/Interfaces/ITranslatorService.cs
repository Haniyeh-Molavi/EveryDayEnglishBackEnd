namespace LanguageLearning.Application.Interfaces;

public interface ITranslatorService
{
    Task<string> TranslateAsync(
        string text,
        string fromLanguage,
        string toLanguage);
}