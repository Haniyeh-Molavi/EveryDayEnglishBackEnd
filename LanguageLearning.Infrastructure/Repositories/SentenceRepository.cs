using LanguageLearning.Application.Interfaces.IRepository;
using LanguageLearning.Domain.Entities;
using LanguageLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearning.Infrastructure.Repositories;

public class SentenceRepository : ISentenceRepository
{
    private readonly ApplicationDbContext _context;

    public SentenceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CommonSentence>> GetAllAsync()
    {
        return await _context.CommonSentences
            .OrderBy(s => s.EnglishText)
            .ToListAsync();
    }

    public async Task<CommonSentence?> GetByIdAsync(Guid id)
    {
        return await _context.CommonSentences.FindAsync(id);
    }

    public async Task AddAsync(CommonSentence sentence)
    {
        _context.CommonSentences.Add(sentence);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CommonSentence sentence)
    {
        _context.CommonSentences.Update(sentence);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CommonSentence sentence)
    {
        _context.CommonSentences.Remove(sentence);
        await _context.SaveChangesAsync();
    }
}
