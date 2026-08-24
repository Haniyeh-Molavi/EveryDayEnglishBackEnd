using MediatR;
using Project.Application.Auth.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearning.Application.Auth.Login
{
    public record LoginCommand(
    string Email,
    string Password
) : IRequest<AuthResponse>;
}
