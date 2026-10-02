using LanguageLearning.Application.Interfaces.IRepository;
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

    public Task AddCategoryAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public Task AddTranslationGroupAsync(TranslationGroup group)
    {
        throw new NotImplementedException();
    }



    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}