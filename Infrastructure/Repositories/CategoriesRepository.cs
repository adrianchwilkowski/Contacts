using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface ICategoriesRepository
    {
        public Task AddCategory(Category category);
        public Task<Category> GetCategory(Guid id);
        public Task<List<Category>> GetCategories();
        public Task AddSubcategory(Subcategory category);
        public Task<Subcategory> GetSubcategory(Guid id);
        public Task<List<Subcategory>> GetSubcategories();

    }

    public class CategoriesRepository : ICategoriesRepository
    {
        private ApplicationDbContext _context { get; set; }
        public CategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(Guid id)
        {
            return await _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task AddSubcategory(Subcategory category)
        {
            await _context.Subcategories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Subcategory>> GetSubcategories()
        {
            return await _context.Subcategories.ToListAsync();
        }

        public async Task<Subcategory> GetSubcategory(Guid id)
        {
            return await _context.Subcategories.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
