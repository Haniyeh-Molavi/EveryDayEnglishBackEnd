using System;
using System.Collections.Generic;
using System.Text;
 using MediatR;
using LanguageLearning.Application.Auth.Common;
namespace LanguageLearning.Application.Auth.Register
{
  
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : IRequest<AuthResponse>;
}
