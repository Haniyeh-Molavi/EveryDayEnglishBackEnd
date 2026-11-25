namespace LanguageLearning.Application.Interfaces.IService;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string email);
}
