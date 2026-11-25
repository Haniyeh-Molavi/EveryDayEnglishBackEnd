using LanguageLearning.Application.DTOs.Words;
using LanguageLearning.Application.Services;
using LanguageLearning.Domain.Enums;
using LanguageLearning.Infrastructure.Persistence;
using LanguageLearning.Infrastructure.Repositories;
using LanguageLearning.Tests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearning.Tests;

public class WordServiceTests
{
    private ApplicationDbContext _context = null!;
    private FakeTranslationService _translation = null!;
    private WordService _service = null!;

    [SetUp]
    public void SetUp()
    {
        _context = TestDbContext.Create();
        _translation = new FakeTranslationService();
        _service = new WordService(
            new WordRepository(_context),
            new CategoryRepository(_context),
            _translation);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task CreateAsync_SavesWordInAllThreeLanguages()
    {
        var result = await _service.CreateAsync(new CreateWordRequest { Word = "gato", Language = Language.Portuguese });

        Assert.That(result.EnglishWord, Is.EqualTo("cat"));
        Assert.That(result.PersianWord, Is.EqualTo("گربه"));
        Assert.That(result.PortugueseWord, Is.EqualTo("gato"));
        Assert.That(result.Type, Is.EqualTo(WordType.Noun));
        Assert.That(result.CategoryName, Is.EqualTo("Animals"));
        Assert.That(result.EnglishExample, Is.EqualTo("The cat is sleeping."));
        Assert.That(await _context.Words.CountAsync(), Is.EqualTo(1));
    }

    [Test]
    public async Task CreateAsync_UsesValuesFromRequest_InsteadOfGemini()
    {
        var request = new CreateWordRequest
        {
            Word = "gata",
            Language = Language.Portuguese,
            Type = WordType.Noun,
            PortugueseGender = Gender.Feminine,
            Category = "Pets"
        };

        var result = await _service.CreateAsync(request);

        Assert.That(result.PortugueseGender, Is.EqualTo(Gender.Feminine));
        Assert.That(result.CategoryName, Is.EqualTo("Pets"));
    }

    [Test]
    public async Task CreateAsync_ReusesExistingCategory()
    {
        await _service.CreateAsync(new CreateWordRequest { Word = "gato", Language = Language.Portuguese });
        await _service.CreateAsync(new CreateWordRequest { Word = "dog", Language = Language.English });

        Assert.That(await _context.Categories.CountAsync(), Is.EqualTo(1));
    }

    [Test]
    public async Task CreateAsync_Throws_WhenWordAlreadyExists()
    {
        await _service.CreateAsync(new CreateWordRequest { Word = "gato", Language = Language.Portuguese });

        Assert.ThrowsAsync<ArgumentException>(() =>
            _service.CreateAsync(new CreateWordRequest { Word = "Gato", Language = Language.Portuguese }));

        // Gemini should not be called for a word we already have
        Assert.That(_translation.CallCount, Is.EqualTo(1));
    }

    [Test]
    public void CreateAsync_Throws_WhenLanguageIsNotValid()
    {
        Assert.ThrowsAsync<ArgumentException>(() =>
            _service.CreateAsync(new CreateWordRequest { Word = "gato", Language = (Language)99 }));
    }

    [Test]
    public async Task GetAllAsync_FindsWordsBySearchText()
    {
        await _service.CreateAsync(new CreateWordRequest { Word = "gato", Language = Language.Portuguese });

        var found = await _service.GetAllAsync("CAT", null);
        var notFound = await _service.GetAllAsync("horse", null);

        Assert.That(found, Has.Count.EqualTo(1));
        Assert.That(notFound, Is.Empty);
    }

    [Test]
    public void DeleteAsync_Throws_WhenWordNotFound()
    {
        Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAsync(Guid.NewGuid()));
    }
}
