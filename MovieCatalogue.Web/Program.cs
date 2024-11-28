using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository.Interfaces;
using MovieCatalogue.Data.Repository;
using MovieCatalogue.Services.Data;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Services.Mapping;
using MovieCatalogue.Web.Infrastructure;
using MovieCatalogue.Web.Infrastructure.Extensions;
using MovieCatalogue.Web.Viewmodels;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("SQLServer");

// Add services to the container.

builder.Services.AddDbContext<MovieDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services
   .AddIdentity<User, IdentityRole<Guid>>(cfg =>
   {
        ConfigureIdentity(builder,cfg);
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

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

WebApplication app = builder.Build();

AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.ApplyMigrations();

app.Run();

static void ConfigureIdentity(WebApplicationBuilder builder, IdentityOptions cfg)
{
 
    cfg.Password.RequireDigit =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
    cfg.Password.RequireLowercase =
        builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
    cfg.Password.RequireUppercase =
        builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
    cfg.Password.RequireNonAlphanumeric =
        builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumerical");
    cfg.Password.RequiredLength =
        builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
    cfg.Password.RequiredUniqueChars =
        builder.Configuration.GetValue<int>("Identity:Password:RequiredUniqueCharacters");

    cfg.SignIn.RequireConfirmedAccount =
        builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
    cfg.SignIn.RequireConfirmedEmail =
        builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
    cfg.SignIn.RequireConfirmedPhoneNumber =
        builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

    cfg.User.RequireUniqueEmail =
        builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");
}

