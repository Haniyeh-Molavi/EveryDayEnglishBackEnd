using LanguageLearning.Application.DTOs.Auth;

namespace LanguageLearning.Application.Interfaces.IService;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}
