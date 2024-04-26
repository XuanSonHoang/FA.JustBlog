using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.IRepositories
{
    public interface IPostRepository
    {
        Post FindPost(int year, int month, string urlSlug);
        Post FindPost(int postId);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);
        void DeletePost(int postId);
        IList<Post> GetAllPosts();
        IList<Post> GetPublisedPosts();
        IList<Post> GetUnpublisedPosts();
        IList<Post> SeachPostByTileAndShortDes(string title, string shortDescription);
        IList<Post> GetLatestPost(int size);
        IList<Post> GetPostsByMonth(DateTime monthYear);
        int CountPostsForCategory(string category);
        IList<Post> GetPostsByCategory(int category);
        int CountPostsForTag(string tag);
        IList<Post> GetPostsByTag(string tag);
        IList<Post> GetMostViewedPost(int size);
        IList<Post> GetHighestPost(int size);
        List<Post> GetPostByViewCount(int v);
    }
}
