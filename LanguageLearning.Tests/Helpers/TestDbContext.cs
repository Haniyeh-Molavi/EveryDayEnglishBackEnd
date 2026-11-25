using LanguageLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearning.Tests.Helpers;

public static class TestDbContext
{
    // Every test gets its own empty in-memory database
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}
