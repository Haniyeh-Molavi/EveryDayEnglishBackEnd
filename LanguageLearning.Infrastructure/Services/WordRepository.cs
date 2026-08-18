using LanguageLearning.Domain.Entities;
using LanguageLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
public class WordRepository : IWordRepository
{
    private readonly ApplicationDbContext _db;

    public WordRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Word word)
    {
        _db.Words.Add(word);

        await _db.SaveChangesAsync();
    }

    public async Task<List<Word>> GetAllAsync()
    {
        return await _db.Words.ToListAsync();
    }
}