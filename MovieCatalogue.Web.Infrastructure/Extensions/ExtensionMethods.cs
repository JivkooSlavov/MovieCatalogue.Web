using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieCatalogue.Data;


namespace MovieCatalogue.Web.Infrastructure.Extensions
{
    public static class ExtensionMethods
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            MovieDbContext dbContext = serviceScope
                .ServiceProvider
                .GetRequiredService<MovieDbContext>()!;
            dbContext.Database.Migrate();

            return app;
        }
    }
}
