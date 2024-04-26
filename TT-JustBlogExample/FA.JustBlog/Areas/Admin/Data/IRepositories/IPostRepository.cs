namespace FA.JustBlog.Areas.Admin.Data.IRepositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts();
        IEnumerable<Post> MostViewedPosts(int count);
        IEnumerable<Post> GetLatestPost(int count);
        IEnumerable<Post> GetMostInterestingPosts(int count);
        IEnumerable<Post> GetPublishedPost(int count);
        IEnumerable<Post> GetUn_PublishedPost(int count);
        Post FindPost(int Id);
        void CreatePost(Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);
        void InsertPost(Post post);
    }
}
