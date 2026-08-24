using LanguageLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LanguageLearning.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Word> Words { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    { }

}