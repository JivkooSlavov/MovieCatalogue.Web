﻿using Microsoft.AspNetCore.Identity;
using Moq;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Services.Data;

[TestFixture]
public class UserServiceTests
{
    private Mock<UserManager<User>> _mockUserManager;
    private Mock<RoleManager<IdentityRole<Guid>>> _mockRoleManager;
    private UserService _userService;

    [SetUp]
    public void SetUp()
    {
        var userStoreMock = new Mock<IUserStore<User>>();
        _mockUserManager = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

        var roleStoreMock = new Mock<IRoleStore<IdentityRole<Guid>>>();
        _mockRoleManager = new Mock<RoleManager<IdentityRole<Guid>>>(roleStoreMock.Object, null, null, null, null);

        _userService = new UserService(_mockUserManager.Object, _mockRoleManager.Object, null);
    }
    [Test]
    public async Task RemoveUserRoleAsync_ShouldReturnFalse_WhenRoleDoesNotExist()
    {
        var userId = Guid.NewGuid();
        var roleName = "NonExistentRole";
        var user = new User { Id = userId, UserName = "testuser", Email = "testuser@example.com" };

        _mockUserManager.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
        _mockRoleManager.Setup(rm => rm.RoleExistsAsync(roleName)).ReturnsAsync(false);

        var result = await _userService.RemoveUserRoleAsync(userId, roleName);

        Assert.IsFalse(result);
        _mockUserManager.Verify(um => um.RemoveFromRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task AssignUserToRoleAsync_ShouldReturnTrue_WhenRoleExists()
    {
        var userId = Guid.NewGuid();
        var roleName = "Admin";

        _mockUserManager.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(new User());
        _mockRoleManager.Setup(rm => rm.RoleExistsAsync(roleName)).ReturnsAsync(true);
        _mockUserManager.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), roleName)).ReturnsAsync(IdentityResult.Success);

        var result = await _userService.AssignUserToRoleAsync(userId, roleName);

        Assert.IsTrue(result);
        _mockUserManager.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), roleName), Times.Once);
    }
    [Test]
    public async Task RemoveUserRoleAsync_ShouldReturnTrue_WhenRoleRemovedSuccessfully()
    {
        var userId = Guid.NewGuid();
        var roleName = "Admin";
        var user = new User { Id = userId, UserName = "testuser", Email = "testuser@example.com" };

        _mockUserManager.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
        _mockRoleManager.Setup(rm => rm.RoleExistsAsync(roleName)).ReturnsAsync(true);
        _mockUserManager.Setup(um => um.IsInRoleAsync(user, roleName)).ReturnsAsync(true);
        _mockUserManager.Setup(um => um.RemoveFromRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Success);

        var result = await _userService.RemoveUserRoleAsync(userId, roleName);

        Assert.IsTrue(result);
        _mockUserManager.Verify(um => um.RemoveFromRoleAsync(user, roleName), Times.Once);
    }

    [Test]
    public async Task DeleteUserAsync_ShouldReturnTrue_WhenUserDeletedSuccessfully()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, UserName = "testuser", Email = "testuser@example.com" };

        _mockUserManager.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
        _mockUserManager.Setup(um => um.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

        var result = await _userService.DeleteUserAsync(userId);

        Assert.IsTrue(result);
        _mockUserManager.Verify(um => um.DeleteAsync(user), Times.Once);
    }

    [Test]
    public async Task DeleteUserAsync_ShouldReturnFalse_WhenDeleteFails()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, UserName = "testuser", Email = "testuser@example.com" };

        _mockUserManager.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
        _mockUserManager.Setup(um => um.DeleteAsync(user)).ReturnsAsync(IdentityResult.Failed());

        var result = await _userService.DeleteUserAsync(userId);

        Assert.IsFalse(result);
        _mockUserManager.Verify(um => um.DeleteAsync(user), Times.Once);
    }
    [Test]
    public async Task DeleteUserAsync_ShouldReturnFalse_WhenUserNotFound()
    {
        var userId = Guid.NewGuid();
        _mockUserManager.Setup(um => um.FindByIdAsync(userId.ToString())).ReturnsAsync((User)null);

        var result = await _userService.DeleteUserAsync(userId);

        Assert.IsFalse(result);
        _mockUserManager.Verify(um => um.DeleteAsync(It.IsAny<User>()), Times.Never);
    }
}