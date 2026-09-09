using LanguageLearning.Domain.Entities;

namespace LanguageLearning.Application.Interfaces;

public interface IWordRepository
{
    Task AddTranslationGroupAsync(
        TranslationGroup translationGroup);
}