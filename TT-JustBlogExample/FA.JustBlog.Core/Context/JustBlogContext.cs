using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FA.JustBlog.Core.Context
{
    public class JustBlogContext : IdentityDbContext<ApplicationUser>, IJustBlogContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostTagMap> PostTagMaps { get; set; }

        public JustBlogContext(DbContextOptions<JustBlogContext> options) :base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // You can config connection string for entity framework in here
            base.OnConfiguring(optionsBuilder);
        }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                String ConnectionStr = config.GetConnectionString("DB");
         
                optionsBuilder.UseSqlServer(ConnectionStr);
            }
        }*/

        // Config fluent api in here
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Config primary key
            modelBuilder.Entity<PostTagMap>().HasKey(pt=> new {pt.TagId,pt.PostId});

            // Config fluent api many tag to many PostTagMaps
            modelBuilder.Entity<PostTagMap>()
                .HasOne<Tag>(pt => pt.Tag)
                .WithMany(p => p.PostTagMaps)
                .HasForeignKey(p => p.TagId);

            // Config fluent api many post to many PostTagMaps
            modelBuilder.Entity<PostTagMap>()
                .HasOne<Post>(pt => pt.Post)
                .WithMany(p => p.PostTagMaps)
                .HasForeignKey(p => p.PostId);

            modelBuilder.Entity<IdentityUserLogin<string>>()
            .HasKey(l => new { l.LoginProvider, l.ProviderKey });

            //change name of table with table have firstmame is "aspnet"
            //example: change table name "aspnetusers" to "appUsers"
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType == typeof(IdentityUser))
                {
                    entityType.SetTableName(entityType.GetTableName().Replace("aspnet", ""));
                }
            }


            // Add seed data
            /* modelBuilder.Entity<Category>().HasData(new Category
             {
                 Id = 1,
                 Name = "Technology",
                 UrlSlug = "technology",
                 Description = "Technology"
             });
             modelBuilder.Entity<Post>().HasData(new Post
             {
                 Id = 1,
                 CategoryId = 1
             });*/
        }
        public override int SaveChanges() => base.SaveChanges();

        //Because it have different signatures so it can't overide
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => base.SaveChangesAsync();
    }
}
