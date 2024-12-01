namespace YourNamespace.Infrastructure.Middleware
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using MovieCatalogue.Data.Models;
    using System.Threading.Tasks;

    public class AdminRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public AdminRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<User> userManager)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var user = await userManager.GetUserAsync(context.User);

                if (user != null && await userManager.IsInRoleAsync(user, "Administrator"))
                {
                    // Пренасочване към Admin/UserManagement/Index
                    context.Response.Redirect("/Admin/UserManagement/Index");
                    return; // Прекратява по-нататъшната обработка
                }
            }

            // Продължава с изпълнението на следващото middleware, ако не е администратор
            await _next(context);
        }
    }
}
