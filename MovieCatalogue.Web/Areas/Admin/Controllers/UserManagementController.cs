using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.Controllers;
using MovieCatalogue.Web.ViewModels.Admin;
using static MovieCatalogue.Common.ApplicationConstants;

namespace MovieCatalogue.Web.Areas.Admin.Controllers
{


    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class UserManagementController : BaseController
    {
        private readonly IUserService _userService;

        public UserManagementController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserViewModel> allUsers = await this._userService
                .GetAllUsersAsync();

            return this.View(allUsers);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            Guid userGuid = Guid.Empty;
            if (!this.IsGuidValid(userId, ref userGuid))
            {
                return this.RedirectToAction(nameof(Index));
            }

            bool userExists = await this._userService
                .UserExistsByIdAsync(userGuid);
            if (!userExists)
            {
                return this.RedirectToAction(nameof(Index));
            }

            bool assignResult = await this._userService
                .AssignUserToRoleAsync(userGuid, role);
            if (!assignResult)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            Guid userGuid = Guid.Empty;
            if (!this.IsGuidValid(userId, ref userGuid))
            {
                return this.RedirectToAction(nameof(Index));
            }

            bool userExists = await this._userService
                .UserExistsByIdAsync(userGuid);
            if (!userExists)
            {
                return this.RedirectToAction(nameof(Index));
            }

            bool removeResult = await this._userService
                .RemoveUserRoleAsync(userGuid, role);
            if (!removeResult)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            Guid userGuid = Guid.Empty;
            if (!this.IsGuidValid(userId, ref userGuid))
            {
                return this.RedirectToAction(nameof(Index));
            }

            bool userExists = await this._userService
                .UserExistsByIdAsync(userGuid);
            if (!userExists)
            {
                return this.RedirectToAction(nameof(Index));
            }

            bool removeResult = await this._userService
                .DeleteUserAsync(userGuid);
            if (!removeResult)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Index));
        }


    }
}
