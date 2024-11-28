using Microsoft.AspNetCore.Http;
using MovieCatalogue.Services.Data.Interfaces;
using System.Security.Claims;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userId != null ? Guid.Parse(userId) : Guid.Empty;
    }

    public string GetUserName()
    {
        return _httpContextAccessor.HttpContext?.User.Identity?.Name ?? string.Empty;
    }
}
