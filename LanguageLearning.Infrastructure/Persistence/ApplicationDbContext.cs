using LanguageLearning.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearning.Infrastructure.Persistence;

// IdentityDbContext already contains the user tables (AspNetUsers, AspNetRoles, ...)
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Word> Words { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<UserWord> UserWords { get; set; }
    public DbSet<CommonSentence> CommonSentences { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Category names are unique, e.g. only one "Animals"
        builder.Entity<Category>()
            .Property(c => c.Name)
            .HasMaxLength(100);

        builder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        // A user can save the same word only once
        builder.Entity<UserWord>()
            .HasKey(uw => new { uw.UserId, uw.WordId });

        builder.Entity<UserWord>()
            .HasOne(uw => uw.User)
            .WithMany(u => u.UserWords)
            .HasForeignKey(uw => uw.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserWord>()
            .HasOne(uw => uw.Word)
            .WithMany()
            .HasForeignKey(uw => uw.WordId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
