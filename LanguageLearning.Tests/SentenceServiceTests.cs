using LanguageLearning.Application.DTOs.Sentences;
using LanguageLearning.Application.Services;
using LanguageLearning.Domain.Enums;
using LanguageLearning.Infrastructure.Persistence;
using LanguageLearning.Infrastructure.Repositories;
using LanguageLearning.Tests.Helpers;

namespace LanguageLearning.Tests;

public class SentenceServiceTests
{
    private ApplicationDbContext _context = null!;
    private SentenceService _service = null!;

    [SetUp]
    public void SetUp()
    {
        _context = TestDbContext.Create();
        _service = new SentenceService(
            new SentenceRepository(_context),
            new FakeTranslationService());
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task CreateAsync_FillsAllThreeLanguages()
    {
        var result = await _service.CreateAsync(new CreateSentenceRequest { Text = "How are you?", Language = Language.English });

        Assert.That(result.EnglishText, Is.EqualTo("How are you?"));
        Assert.That(result.PersianText, Is.EqualTo("حالت چطور است؟"));
        Assert.That(result.PortugueseText, Is.EqualTo("Como estás?"));
        Assert.That(result.IsActive, Is.True);
    }

    [Test]
    public async Task UpdateAsync_ChangesTheTexts()
    {
        var created = await _service.CreateAsync(new CreateSentenceRequest { Text = "How are you?", Language = Language.English });

        var request = new UpdateSentenceRequest
        {
            EnglishText = "How are you doing?",
            PersianText = "چطوری؟",
            PortugueseText = "Como vais?",
            IsActive = false
        };

        await _service.UpdateAsync(created.Id, request);
        var updated = await _service.GetByIdAsync(created.Id);

        Assert.That(updated.EnglishText, Is.EqualTo("How are you doing?"));
        Assert.That(updated.IsActive, Is.False);
    }

    [Test]
    public void GetByIdAsync_Throws_WhenSentenceNotFound()
    {
        Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(Guid.NewGuid()));
    }
}
