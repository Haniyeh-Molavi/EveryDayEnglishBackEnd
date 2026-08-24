using LanguageLearning.Application.Interfaces;
using LanguageLearning.Domain.Entities;
using LanguageLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
public class WordRepository : IWordRepository
{
    private readonly ApplicationDbContext _context;

    public WordRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddTranslationGroupAsync(TranslationGroup group)
    {
       _context.Add(group);

        await _context.SaveChangesAsync();
    }
}