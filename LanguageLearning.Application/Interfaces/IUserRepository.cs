using System;
using System.Collections.Generic;
using System.Text;
using LanguageLearning.Domain.Entities;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
}