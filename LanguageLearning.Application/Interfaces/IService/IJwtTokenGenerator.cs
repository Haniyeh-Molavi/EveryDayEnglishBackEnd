using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearning.Application.Interfaces.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId, string email);
    }
}
