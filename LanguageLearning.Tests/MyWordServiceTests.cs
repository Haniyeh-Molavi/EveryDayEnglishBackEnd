using LanguageLearning.Application.Services;
using LanguageLearning.Domain.Entities;
using LanguageLearning.Domain.Enums;
using LanguageLearning.Infrastructure.Persistence;
using LanguageLearning.Infrastructure.Repositories;
using LanguageLearning.Tests.Helpers;

namespace LanguageLearning.Tests;

public class MyWordServiceTests
{
    private ApplicationDbContext _context = null!;
    private MyWordService _service = null!;

    private readonly Guid _userId = Guid.NewGuid();
    private Word _cat = null!;
    private Word _dog = null!;

    [SetUp]
    public void SetUp()
    {
        _context = TestDbContext.Create();
        _service = new MyWordService(
            new UserWordRepository(_context),
            new WordRepository(_context));

        // Two words that already exist in the database
        var animals = new Category { Id = Guid.NewGuid(), Name = "Animals" };
        _cat = CreateWord("cat", animals);
        _dog = CreateWord("dog", animals);

        _context.Categories.Add(animals);
        _context.Words.AddRange(_cat, _dog);
        _context.SaveChanges();
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task AddAsync_SavesWordToMyList()
    {
        await _service.AddAsync(_userId, _cat.Id);

        var myWords = await _service.GetMyWordsAsync(_userId, null);

        Assert.That(myWords, Has.Count.EqualTo(1));
        Assert.That(myWords[0].Word.EnglishWord, Is.EqualTo("cat"));
        Assert.That(myWords[0].IsLearned, Is.False);
    }

    [Test]
    public async Task AddAsync_Throws_WhenWordIsAlreadyInMyList()
    {
        await _service.AddAsync(_userId, _cat.Id);

        Assert.ThrowsAsync<ArgumentException>(() => _service.AddAsync(_userId, _cat.Id));
    }

    [Test]
    public void AddAsync_Throws_WhenWordDoesNotExist()
    {
        Assert.ThrowsAsync<KeyNotFoundException>(() => _service.AddAsync(_userId, Guid.NewGuid()));
    }

    [Test]
    public async Task UpdateProgressAsync_MarksWordAsLearned()
    {
        await _service.AddAsync(_userId, _cat.Id);
        await _service.AddAsync(_userId, _dog.Id);

        var result = await _service.UpdateProgressAsync(_userId, _cat.Id, true);
        var progress = await _service.GetProgressAsync(_userId);

        Assert.That(result.IsLearned, Is.True);
        Assert.That(result.LearnedAt, Is.Not.Null);
        Assert.That(progress.TotalWords, Is.EqualTo(2));
        Assert.That(progress.LearnedWords, Is.EqualTo(1));
        Assert.That(progress.NotLearnedWords, Is.EqualTo(1));
    }

    [Test]
    public async Task GetMyWordsAsync_ReturnsOnlyWordsStillToLearn()
    {
        await _service.AddAsync(_userId, _cat.Id);
        await _service.AddAsync(_userId, _dog.Id);
        await _service.UpdateProgressAsync(_userId, _cat.Id, true);

        var toLearn = await _service.GetMyWordsAsync(_userId, false);

        Assert.That(toLearn, Has.Count.EqualTo(1));
        Assert.That(toLearn[0].Word.EnglishWord, Is.EqualTo("dog"));
    }

    [Test]
    public async Task RemoveAsync_RemovesWordFromMyList()
    {
        await _service.AddAsync(_userId, _cat.Id);

        await _service.RemoveAsync(_userId, _cat.Id);

        Assert.That(await _service.GetMyWordsAsync(_userId, null), Is.Empty);
    }

    [Test]
    public async Task OtherUsersDoNotSeeMyWords()
    {
        await _service.AddAsync(_userId, _cat.Id);

        var otherUserWords = await _service.GetMyWordsAsync(Guid.NewGuid(), null);

        Assert.That(otherUserWords, Is.Empty);
    }

    private static Word CreateWord(string english, Category category)
    {
        return new Word
        {
            Id = Guid.NewGuid(),
            EnglishWord = english,
            PersianWord = english + " (fa)",
            PortugueseWord = english + " (pt)",
            Type = WordType.Noun,
            CategoryId = category.Id,
            Category = category,
            CreatedAt = DateTime.UtcNow
        };
    }
}
