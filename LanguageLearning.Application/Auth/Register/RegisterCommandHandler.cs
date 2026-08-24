using System;
using System.Collections.Generic;
using System.Text;
using global::Project.Application.Auth.Common;
using LanguageLearning.Application.Interfaces;
using LanguageLearning.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LanguageLearning.Application.Auth.Register
{

    public class RegisterCommandHandler: IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtGenerator;

        public RegisterCommandHandler(
            UserManager<ApplicationUser> userManager,
            IJwtTokenGenerator jwtGenerator)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<AuthResponse> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await _userManager.CreateAsync(
                user,
                request.Password);

            if (!result.Succeeded)
                throw new Exception(
                    string.Join(",",
                    result.Errors.Select(x => x.Description)));

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
