using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Services.Data;
using MovieCatalogue.Web.Viewmodels;
using MovieCatalogue.Web.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using MovieCatalogue.Data.Configuration;
using MovieCatalogue.Data.Repository.Interfaces;
using MovieCatalogue.Data.Repository;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("SQLServer")!;
string adminEmail = builder.Configuration.GetValue<string>("Administrator:Email")!;
string adminUsername = builder.Configuration.GetValue<string>("Administrator:Username")!;
string adminPassword = builder.Configuration.GetValue<string>("Administrator:Password")!;

// Add services to the container.
builder.Services.AddDbContext<MovieDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services
   .AddIdentity<User, IdentityRole<Guid>>(cfg =>
   {
       ConfigureIdentity(builder, cfg);
   })
   .AddEntityFrameworkStores<MovieDbContext>()
   .AddRoles<IdentityRole<Guid>>()
   .AddSignInManager<SignInManager<User>>()
   .AddUserManager<UserManager<User>>();

builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.LoginPath = "/Identity/Account/Login";
});

builder.Services.RegisterRepositories(typeof(User).Assembly);
builder.Services.RegisterUserDefinedServices(typeof(IReviewService).Assembly);

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IRepository<User, Guid>, BaseRepository<User, Guid>>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();

//app.UseRoleRedirectMiddleware();

app.UseAuthorization();

app.SeedAdministrator(adminEmail, adminUsername, adminPassword);

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.ApplyMigrations();

app.Run();

// ConfigureIdentity method as before
static void ConfigureIdentity(WebApplicationBuilder builder, IdentityOptions cfg)
{
    cfg.Password.RequireDigit =
        builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
    cfg.Password.RequireLowercase =
        builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
    cfg.Password.RequireUppercase =
        builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
    cfg.Password.RequireNonAlphanumeric =
        builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
    cfg.Password.RequiredLength =
        builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
    cfg.Password.RequiredUniqueChars =
        builder.Configuration.GetValue<int>("Identity:Password:RequireUniqueChars");

    cfg.SignIn.RequireConfirmedAccount =
        builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
    cfg.SignIn.RequireConfirmedEmail =
        builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
    cfg.SignIn.RequireConfirmedPhoneNumber =
        builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

    cfg.User.RequireUniqueEmail =
        builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");
}

// Create middleware for redirecting admin users
public static class AdminRedirectMiddleware
{
    public static void UseAdminRedirect(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
                var user = await userManager.GetUserAsync(context.User);

                if (user != null && await userManager.IsInRoleAsync(user, "Administrator"))
                {
                    // Redirect admin users to /Admin/Home/Index
                    context.Response.Redirect("/Admin/Home/Index");
                    return;
                }
            }

            await next();
        });
    }
}
