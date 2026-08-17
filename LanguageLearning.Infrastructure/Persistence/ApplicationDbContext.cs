using LanguageLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearning.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
}