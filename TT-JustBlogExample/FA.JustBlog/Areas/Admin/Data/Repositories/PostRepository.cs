using FA.JustBlog.Areas.Admin.Data.IRepositories;
using FA.JustBlog.Core.Context;

namespace FA.JustBlog.Areas.Admin.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly JustBlogContext _context;
        public PostRepository(JustBlogContext context)
        {
            _context = context;
        }
        public void CreatePost(Post post)
        {
            _context.Add(post);
            _context.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            _context.Remove(post);
            _context.SaveChanges();
        }

        public Post FindPost(int Id)
        {
            return _context.Posts.Find(Id);
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public IEnumerable<Post> GetLatestPost(int count)
        {
            return _context.Posts.OrderByDescending(p => p.PostedOn).Take(count).ToList();
        }

        public IEnumerable<Post> GetMostInterestingPosts(int count)
        {
            return _context.Posts.OrderByDescending(p => p.Rate).Take(count).ToList();
        }

        public IEnumerable<Post> GetPublishedPost(int count)
        {
            return _context.Posts.Where(p => p.Published).OrderByDescending(p => p.PostedOn).Take(count).ToList();
        }

        public IEnumerable<Post> GetUn_PublishedPost(int count)
        {
            return _context.Posts.Where(p => !p.Published).OrderByDescending(p => p.PostedOn).Take(count).ToList();
        }

        public void InsertPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public IEnumerable<Post> MostViewedPosts(int count)
        {
            return _context.Posts.OrderByDescending(p => p.ViewCount).Take(count).ToList();
        }

        public void UpdatePost(Post post)
        {
            _context.Update(post);
            _context.SaveChanges();
        }
    }
}
