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
    public class PostRepository : IPostRepository
    {
        private readonly IJustBlogContext _context;
        public PostRepository(IJustBlogContext context)
        {
            _context = context;
        }
        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public int CountPostsForCategory(string category)
        {
            return _context.Posts.Count(p => p.Category.Name == category);
        }

        public int CountPostsForTag(string tag)
        {
            return _context.Posts.Count(p => p.PostTagMaps.Any(ptm => ptm.Tag.Name == tag));
        }

        public void DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public void DeletePost(int postId)
        {
            Post postDel = _context.Posts.Find(postId);
            if (postDel != null)
            {
                _context.Posts.Remove(postDel);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Post not found");
            }
        }

        public Post FindPost(int year, int month, string title)
        {
            return _context.Posts.FirstOrDefault(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.Title == title);
        }

        public Post FindPost(int postId)
        {
            return _context.Posts.Find(postId);
        }

        public IList<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public IList<Post> GetHighestPost(int size)
        {
            return _context.Posts.OrderByDescending(p => p.Rate).Take(size).ToList();
        }

        public IList<Post> GetLatestPost(int size)
        {
            return _context.Posts.OrderByDescending(p => p.PostedOn).Take(size).ToList();
        }

        public IList<Post> GetMostViewedPost(int size)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetPostByViewCount(int v)
        {
            return _context.Posts.OrderByDescending(p => p.ViewCount).Take(v).ToList();
        }

        public IList<Post> GetPostsByCategory(int category)
        {
            return _context.Posts.Where(p => p.Category.Id == category).ToList();
        }

        public IList<Post> GetPostsByMonth(DateTime monthYear)
        {
            throw new NotImplementedException();
        }

        public IList<Post> GetPostsByTag(string tag)
        {
            throw new NotImplementedException();
        }

        public IList<Post> GetPublisedPosts()
        {
            throw new NotImplementedException();
        }

        public IList<Post> GetUnpublisedPosts()
        {
            throw new NotImplementedException();
        }

        public IList<Post> SeachPostByTileAndShortDes(string title, string shortDescription)
        {
            return _context.Posts.Where(p => p.Title.Contains(title) || p.ShortDescription.Contains(shortDescription)).ToList();
        }

        public void UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
