using LanguageLearning.Application.Common.Models;
using LanguageLearning.Application.Interfaces;
using LanguageLearning.Domain.Entities;
using LanguageLearning.Infrastructure.Authentication;
using LanguageLearning.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.Text;

namespace LanguageLearning.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
IConfiguration configuration)
    {
        services.AddScoped<IWordRepository, WordRepository>();

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        var jwtSettings = configuration.GetSection("JwtSettings");
  
services.Configure<JwtSettings>(jwtSettings);

var key = Encoding.UTF8.GetBytes(
jwtSettings["Secret"]!);
services.AddAuthentication(options =>
{
options.DefaultAuthenticateScheme =
JwtBearerDefaults.AuthenticationScheme;
options.DefaultChallengeScheme =
JwtBearerDefaults.AuthenticationScheme;      
})
.AddJwtBearer(options =>
{
options.TokenValidationParameters =
new TokenValidationParameters
{     
ValidateIssuer = true,
ValidateAudience = true,
ValidateLifetime = true,

ValidateIssuerSigningKey = true,

ValidIssuer = jwtSettings["Issuer"],
ValidAudience = jwtSettings["Audience"],

IssuerSigningKey =
new SymmetricSecurityKey(key)};
            
});

services.AddScoped < IJwtTokenGenerator,JwtTokenGenerator > ();

        return services;
    }
}