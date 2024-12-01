using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MovieCatalogue.Data.Models;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.Infrastructure.Middlewares;

public class RoleRedirectMiddleware
{
    private readonly RequestDelegate _next;

    public RoleRedirectMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, UserManager<User> userManager)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user != null)
            {
                var isAdmin = await userManager.IsInRoleAsync(user, "Admin");

                if (isAdmin && !context.Request.Path.StartsWithSegments("/Admin"))
                {
                    context.Response.Redirect("/Admin/Home/Index");
                    return; 
                }
            }
        }

        await _next(context);
    }
}
