using LanguageLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LanguageLearning.Infrastructure.Persistence;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

//public class ApplicationDbContext : DbContext
//{
//    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//        : base(options)
//    {
//    }

//    public DbSet<Word> Words { get; set; }

//    protected override void OnModelCreating(ModelBuilder builder)
//    { }

//}

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Word> Words { get; set; }
    public DbSet<UserWord> UserWords { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CommonSentence> CommonSentences { get; set; }
    public DbSet<TranslationGroup> TranslationGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserWord>()
            .HasKey(uw => new { uw.UserId, uw.WordId });

        builder.Entity<UserWord>()
            .HasOne(uw => uw.User)
            .WithMany(u => u.Words)
            .HasForeignKey(uw => uw.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserWord>()
            .HasOne(uw => uw.Word)
            .WithMany()
            .HasForeignKey(uw => uw.WordId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}