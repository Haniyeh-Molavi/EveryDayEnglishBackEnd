using LanguageLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));