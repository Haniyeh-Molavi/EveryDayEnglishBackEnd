using MediatR;
using LanguageLearning.Application.Auth.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearning.Application.Auth.Login
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

