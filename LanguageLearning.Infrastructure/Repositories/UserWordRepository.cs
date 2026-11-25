using LanguageLearning.Application.Interfaces.IRepository;
using LanguageLearning.Domain.Entities;
using LanguageLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearning.Infrastructure.Repositories;

public class UserWordRepository : IUserWordRepository
{
    private readonly ApplicationDbContext _context;

    public UserWordRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserWord>> GetByUserAsync(Guid userId, bool? isLearned)
    {
        var query = _context.UserWords
            .Include(uw => uw.Word)
            .ThenInclude(w => w.Category)
            .Where(uw => uw.UserId == userId);

        if (isLearned != null)
        {
            query = query.Where(uw => uw.IsLearned == isLearned);
        }

        // Newest saved words first
        return await query
            .OrderByDescending(uw => uw.SavedAt)
            .ToListAsync();
    }

    public async Task<UserWord?> GetAsync(Guid userId, Guid wordId)
    {
        return await _context.UserWords
            .Include(uw => uw.Word)
            .ThenInclude(w => w.Category)
            .FirstOrDefaultAsync(uw => uw.UserId == userId && uw.WordId == wordId);
    }

    public async Task AddAsync(UserWord userWord)
    {
        _context.UserWords.Add(userWord);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserWord userWord)
    {
        _context.UserWords.Update(userWord);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserWord userWord)
    {
        _context.UserWords.Remove(userWord);
        await _context.SaveChangesAsync();
    }
}
