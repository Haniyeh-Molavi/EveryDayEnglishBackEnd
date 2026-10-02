using LanguageLearning.Application.Interfaces.IRepository;
using LanguageLearning.Domain.Entities;
using LanguageLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageLearning.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository

    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public Task AddTranslationGroupAsync(TranslationGroup group)
        {
            throw new NotImplementedException();
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories
         .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
    }