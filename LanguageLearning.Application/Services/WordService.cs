using LanguageLearning.Application.Interfaces;
using LanguageLearning.Application.Mappings;
using LanguageLearning.Domain.Entities;
using LanguageLearning.Domain.Enums;
using LanguageLearning.Infrastructure.ExternalServices.AzureTranslator;

namespace LanguageLearning.Application.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _wordRepository;
    private readonly TranslatorService _translator;
    private readonly IWordMetadataService _wordMetadataService;

    public WordService(
        IWordRepository wordRepository,
        TranslatorService translator,
        IWordMetadataService wordMetadataService)
    {
        _wordRepository = wordRepository;
        _translator = translator;
        _wordMetadataService = wordMetadataService;
    }

    public async Task CreateAsync(TranslationDto dto)
    {
    
    }
}
