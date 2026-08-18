using LanguageLearning.Application.Interfaces;
using LanguageLearning.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LanguageLearning.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IWordService, WordService>();

        return services;
    }
}