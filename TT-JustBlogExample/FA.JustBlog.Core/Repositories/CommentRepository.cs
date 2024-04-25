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
    public class CommentRepository : ICommentRepository
    {
        private readonly JustBlogContext _context;
        public CommentRepository(JustBlogContext context)
        {
            _context = context;
        }
        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void AddComment(int postId, string commentName, string commentEmail, string commentTitle, string commentBody)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }

        public void DeleteComment(int commendId)
        {
            _context.Comments.Remove(Find(commendId));
            _context.SaveChanges();
        }

        public Comment Find(int commentId)
        {
            return _context.Comments.Find(commentId);
        }

        public IList<Comment> GetAllComments()
        {
            return _context.Comments.ToList();
        }

        public IList<Comment> GetCommentsForPost(int postId)
        {
            return _context.Comments.Where(c => c.PostId == postId).ToList();
        }

        public IList<Comment> GetCommentsForPost(Post post)
        {
            throw new NotImplementedException();
        }

        public void UpdateComment(Comment comment)
        {
            _context.Comments.Update(comment);
            _context.SaveChanges();
        }
    }
}
