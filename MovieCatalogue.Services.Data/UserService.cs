﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Services.Data;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.ViewModels.Admin;
using System.Security.Claims;

public class UserService : BaseService, IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _roleManager = roleManager;
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
    public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
    {
        IEnumerable<User> allUsers = await _userManager.Users
            .ToArrayAsync();
        ICollection<UserViewModel> allUsersViewModel = new List<UserViewModel>();

        foreach (User user in allUsers)
        {
            IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

            allUsersViewModel.Add(new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Roles = roles.ToList()
            });
        }

        return allUsersViewModel;
    }

    public async Task<bool> UserExistsByIdAsync(Guid userId)
    {
        User? user = await _userManager
            .FindByIdAsync(userId.ToString());

        return user != null;
    }

    public async Task<bool> AssignUserToRoleAsync(Guid userId, string roleName)
    {
        User? user = await _userManager
            .FindByIdAsync(userId.ToString());
        bool roleExists = await _roleManager.RoleExistsAsync(roleName);

        if (user == null || !roleExists)
        {
            return false;
        }

        bool alreadyInRole = await _userManager.IsInRoleAsync(user, roleName);
        if (!alreadyInRole)
        {
            IdentityResult? result = await _userManager
                .AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                return false;
            }
        }

        return true;
    }

    public async Task<bool> RemoveUserRoleAsync(Guid userId, string roleName)
    {
        User? user = await _userManager
            .FindByIdAsync(userId.ToString());
        bool roleExists = await _roleManager.RoleExistsAsync(roleName);

        if (user == null || !roleExists)
        {
            return false;
        }

        bool alreadyInRole = await _userManager.IsInRoleAsync(user, roleName);
        if (alreadyInRole)
        {
            IdentityResult? result = await _userManager
                .RemoveFromRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                return false;
            }
        }

        return true;
    }

    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        User? user = await _userManager
            .FindByIdAsync(userId.ToString());

        if (user == null)
        {
            return false;
        }

        IdentityResult? result = await _userManager
            .DeleteAsync(user);
        if (!result.Succeeded)
        {
            return false;
        }

        return true;
    }


}