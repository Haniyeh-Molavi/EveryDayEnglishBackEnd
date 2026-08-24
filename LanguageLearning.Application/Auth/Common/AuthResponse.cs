using System;
using System.Collections.Generic;
using System.Text;
namespace Project.Application.Auth.Common;

public class AuthResponse
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}