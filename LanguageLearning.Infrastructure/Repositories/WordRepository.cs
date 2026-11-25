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
}
