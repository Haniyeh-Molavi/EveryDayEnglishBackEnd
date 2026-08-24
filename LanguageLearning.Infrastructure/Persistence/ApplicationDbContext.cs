using LanguageLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LanguageLearning.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Word> Words => Set<Word>();
    public DbSet<Category> Categories { get; set; }
    public DbSet<TranslationGroup> TranslationGroups
        => Set<TranslationGroup>();

    protected override void OnModelCreating(ModelBuilder builder)
    { }

}