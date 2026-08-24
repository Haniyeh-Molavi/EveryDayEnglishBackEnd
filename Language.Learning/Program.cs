using LanguageLearning.Application;
using LanguageLearning.Application.Interfaces;
using LanguageLearning.Application.Services;
using LanguageLearning.Infrastructure;
using LanguageLearning.Infrastructure.ExternalServices.AzureTranslator;
using LanguageLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ITranslatorService, TranslatorService>();
builder.Services.AddScoped<IWordService, WordService>();

builder.Services.AddScoped<IWordRepository, WordRepository>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
