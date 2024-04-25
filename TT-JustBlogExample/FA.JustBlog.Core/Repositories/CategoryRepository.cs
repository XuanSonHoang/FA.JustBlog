using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IJustBlogContext _context;
        public CategoryRepository(IJustBlogContext context)
        {
            _context = context;
        }
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        //Implemnt async/await method
        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            Category category = Find(categoryId);
            Post post = _context.Posts.FirstOrDefault(p => p.CategoryId == categoryId);
            if (category != null && post != null)
            {
                _context.Posts.Remove(post);
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Category not found");
            }
        }

        public async Task DeleteCategoryAsync(Category cate)
        {
            
            _context.Categories.Remove(cate);
            await _context.SaveChangesAsync();
        }

        public Category Find(int categoryId)
        {
            return _context.Categories.Find(categoryId);
        }

        public IList<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
    }
}
