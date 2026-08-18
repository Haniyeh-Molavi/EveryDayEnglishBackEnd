using LanguageLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearning.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Word> Words => Set<Word>();

    public DbSet<TranslationGroup> TranslationGroups
        => Set<TranslationGroup>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Word>()
            .Property(x => x.Text)
            .HasMaxLength(200);

        builder.Entity<Word>()
            .HasOne(x => x.TranslationGroup)
            .WithMany(x => x.Words)
            .HasForeignKey(x => x.TranslationGroupId);

        builder.Entity<Word>()
            .HasIndex(x => new
            {
                x.Text,
                x.Language
            });
    }
}