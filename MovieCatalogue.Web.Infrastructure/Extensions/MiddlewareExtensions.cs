using Microsoft.AspNetCore.Builder;
using MovieCatalogue.Web.Infrastructure.Middlewares;

namespace MovieCatalogue.Web.Infrastructure.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRoleRedirectMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RoleRedirectMiddleware>();
        }
    }
}
