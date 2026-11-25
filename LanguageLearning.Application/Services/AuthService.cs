using LanguageLearning.Application.DTOs.Auth;
using LanguageLearning.Application.Interfaces.IService;
using LanguageLearning.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace LanguageLearning.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            // For example: "Passwords must have at least one digit ('0'-'9')."
            var errors = string.Join(" ", result.Errors.Select(e => e.Description));
            throw new ArgumentException(errors);
        }

        return CreateAuthResponse(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            throw new UnauthorizedAccessException("Invalid email or password.");

        return CreateAuthResponse(user);
    }

    private AuthResponse CreateAuthResponse(ApplicationUser user)
    {
        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email!,
            Token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email!)
        };
    }
}
