using LanguageLearning.Application.Interfaces.IService;
using LanguageLearning.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LanguageLearning.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IWordService, WordService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IMyWordService, MyWordService>();
        services.AddScoped<ISentenceService, SentenceService>();

        return services;
    }
}
