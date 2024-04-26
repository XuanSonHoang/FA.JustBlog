using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FA.JustBlog.Core.Infrastructure
{
    public static class InitialService
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IJustBlogContext, JustBlogContext>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            return services;
        }
    }
}
