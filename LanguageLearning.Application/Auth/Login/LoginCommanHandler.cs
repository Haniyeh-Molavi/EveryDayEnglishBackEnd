using LanguageLearning.Application.Interfaces;
using LanguageLearning.Domain.Entities;
using MediatR;
using Microsoft.AspNet.Identity;
using LanguageLearning.Application.Auth.Login;
using Microsoft.AspNet.Identity.EntityFramework;
using LanguageLearning.Application.Auth.Common;

namespace LanguageLearning.Application.Auth.Login
{
    public class LoginCommandHandler
        : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtGenerator;

        public LoginCommandHandler(
            UserManager<ApplicationUser> userManager,
            IJwtTokenGenerator jwtGenerator)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<AuthResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager
                .FindByEmailAsync(request.Email);

            if (user is null)
                throw new Exception("Invalid credentials");

            var validPassword =
                await _userManager.CheckPasswordAsync(
                    user,
                    request.Password);

            if (!validPassword)
                throw new Exception("Invalid credentials");

            var token = _jwtGenerator.GenerateToken(
                user.Id,
                user.Email!);

            return new AuthResponse
            {
                UserId = user.Id,
                Email = user.Email!,
                Token = token
            };
        }
    }
}
