using System;
using System.Collections.Generic;
using System.Text;
namespace LanguageLearning.Application.Auth.Common;

public class AuthResponse
{
    public Guid UserId { get; set; } = Guid.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}