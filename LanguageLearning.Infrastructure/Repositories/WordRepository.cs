using LanguageLearning.Application.Interfaces.IRepository;
using LanguageLearning.Domain.Entities;
using LanguageLearning.Domain.Enums;
using LanguageLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearning.Infrastructure.Repositories;

public class WordRepository : IWordRepository
{
    private readonly ApplicationDbContext _context;

    public WordRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Word>> GetAllAsync(string? search, Guid? categoryId)
    {
        var query = _context.Words
            .Include(w => w.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim().ToLower();

            // Search in all three languages
            query = query.Where(w =>
                w.EnglishWord.ToLower().Contains(search) ||
                w.PersianWord.ToLower().Contains(search) ||
                w.PortugueseWord.ToLower().Contains(search));
        }

        if (categoryId != null)
        {
            query = query.Where(w => w.CategoryId == categoryId);
        }

        return await query
            .OrderBy(w => w.EnglishWord)
            .ToListAsync();
    }

    public async Task<Word?> GetByIdAsync(Guid id)
    {
        return await _context.Words
            .Include(w => w.Category)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<bool> ExistsAsync(string text, Language language)
    {
        text = text.ToLower();

        return language switch
        {
            Language.English => await _context.Words.AnyAsync(w => w.EnglishWord.ToLower() == text),
            Language.Portuguese => await _context.Words.AnyAsync(w => w.PortugueseWord.ToLower() == text),
            Language.Persian => await _context.Words.AnyAsync(w => w.PersianWord.ToLower() == text),
            _ => false
        };
    }

    public async Task AddAsync(Word word)
    {
        _context.Words.Add(word);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Word word)
    {
        _context.Words.Remove(word);
        await _context.SaveChangesAsync();
    }
}
