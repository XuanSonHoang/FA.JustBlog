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
    public class TagRepository : ITagRepository
    {
        private readonly IJustBlogContext _context;
        public TagRepository(IJustBlogContext context)
        {
            _context = context;
        }
        public void AddTag(Tag Tag)
        {
            _context.Tags.Add(Tag);
            _context.SaveChanges();
        }

        public void DeleteTag(Tag Tag)
        {
            throw new NotImplementedException();
        }

        public void DeleteTag(int TagId)
        {
            throw new NotImplementedException();
        }

        public Tag Find(int TagId)
        {
            throw new NotImplementedException();
        }

        public IList<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }

        public Tag GetTagByUrlSlug(string urlSlug)
        {
            throw new NotImplementedException();
        }

        public void UpdateTag(Tag Tag)
        {
            throw new NotImplementedException();
        }
    }
}
