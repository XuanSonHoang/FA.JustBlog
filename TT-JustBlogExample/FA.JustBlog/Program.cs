using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.Infrastructure;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using FA.JustBlog.Services;
using log4net.Config;
using log4net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FA.JustBlog.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Configure log4net
var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
builder.Services.AddScoped<LoggerHelper>();

// ... rest of the code ...

builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.ClearProviders();
    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
});
// Add services to the container.
builder.Services.AddControllersWithViews();

//Get connectionn string from file config
string connnectionString = builder.Configuration.GetConnectionString("SQLConnection");

builder.Services.AddDbContext<JustBlogContext>(opts => {
    // Set up connection string for db context
    opts.UseSqlServer(connnectionString);
});

// Use DI (Dependency injection)
builder.Services.AddScoped<FA.JustBlog.Areas.Admin.Data.IRepositories.IPostRepository, FA.JustBlog.Areas.Admin.Data.Repositories.PostRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IJustBlogContext, JustBlogContext>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddSingleton<log4net.ILog>(_ => log4net.LogManager.GetLogger(typeof(Program)));

// Call static metod to init di
builder.Services.AddInfrastructureServices();

//add entity for manage login
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<JustBlogContext>()
    .AddDefaultTokenProviders();
builder.Services.AddRazorPages();

//config multiple language
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
builder.Services.Configure<RequestLocalizationOptions>(options => {
    var supportedCultures = new[] { "en", "vn" };
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});

//config mail setting service
builder.Services.AddOptions();
var MailSettings = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(MailSettings);
builder.Services.AddTransient<IEmailSender, SendEmailService>();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;

    //email setting
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
    options.SignIn.RequireConfirmedAccount = true;
});

builder.Services.ConfigureApplicationCookie(options => {
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Account/Login";

    options.LogoutPath = "/Account/Logout";
    options.SlidingExpiration = true;
});

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//use static file
app.UseStaticFiles();

app.UseRequestLocalization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseAuthentication();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "postList",
    pattern: "{controller=Post}/{action=Index}"
);

app.MapControllerRoute(
    name: "Post",
    "Post/{year}/{mounth}/{title}",
    new { controller = " Post", action = "Details" },
    new { year = @"\d{4}", mounth = @"\d{2}" }
);

app.MapControllerRoute(
    name: "Category",
    pattern: "category/Index/{id?}",
    new { controller = "Category", action = "Index" }
);

app.MapControllerRoute(
       name: "Role_Delete",
          pattern: "Admin/Role/Delete/{id?}",
             new { controller = "Role", action = "Delete" }
             );

app.MapControllerRoute(
    name: "Tag",
    pattern: "tag/PopularTags/{id?}",
    new { controller = "Tag", action = "PopularTags" }
    );

app.MapControllerRoute(
    name: "Admin_Create",
    pattern: "Admin/Post/Create",
    new { controller = "Post", action = "Create" }
);

app.MapControllerRoute(
    name: "Admin_Create",
    pattern: "Admin/Category/List",
    new { controller = "Category", action = "List" }
);

app.MapControllerRoute(
    name: "Admin_Delete",
    pattern: "Admin/Post/DeletePost/{id?}",
    new { controller = "Post", action = "DeletePost" }
);
app.MapControllerRoute(
    name: "Admin_AllList",
    pattern: "Admin/Post/List",
    new { controller = "Post", action = "List" }
);

app.MapControllerRoute(
       name: "Admin_Update",
          pattern: "Admin/Post/Update/{id?}",
             new { controller = "Post", action = "UpdatePost" }
             );
app.MapControllerRoute(
    name: "Admin_jqGrid",
    pattern: "Admin/Post/ListUsingJqGrid",
    new { controller = "Post", action = "ListUsingJqGrid" }
);

app.MapControllerRoute(
    name: "Admin_Paging",
    pattern: "Admin/Post/List",
    new { controller = "Post", action = "List" }
);
app.MapControllerRoute(
    name: "Post_Paging",
    pattern: "Post/Index",
    new { controller = "Post", action = "Index" }
);
//area of admin 

app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(
      name: "Post",
      pattern: "{area:exists}/{controller=Post}/{action=PostsAction}/{id?}"
    );
});

app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(
      name: "Account",
      pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}"
    );
});

app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(
      name: "Category",
      pattern: "{area:exists}/{controller=Category}/{action=List}/{id?}"
    );
});


app.MapRazorPages();

app.Run();
