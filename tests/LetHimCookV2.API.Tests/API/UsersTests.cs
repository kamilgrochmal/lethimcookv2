using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using LetHimCookV2.API.Application.DTO;
using LetHimCookV2.API.Application.Requests;
using LetHimCookV2.API.Domain.Abstractions;
using LetHimCookV2.API.Domain.Patients;
using LetHimCookV2.API.Domain.Users;
using LetHimCookV2.API.Infrastructure.Security;
using LetHimCookV2.API.Infrastructure.Time;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LetHimCookV2.API.Tests.API;

public class UsersTests : ControllerTests, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public UsersTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }


    public void Dispose()
    {
        _testDatabase.Dispose();
    }

    [Fact]
    public async Task POST_users_validData_Returns204()
    {
        await _testDatabase.Context.Database.MigrateAsync();
        var command = new SignUp("test-user1@lethimcookv2.io", "test-user1", "first", "second",
            new DateTime(2000, 4, 1), "123123123", "secret", "user");
        var response = await Client.PostAsJsonAsync("Users", command);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task POST_signIn_ValidData_ReturnsJWTToken()
    {
        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        var clock = new Clock();
        const string password = "secret";
        var signUp = new SignUp("test@lethimcookv2.com", "cooker", "first", "second", new DateTime(2000, 4, 1),
            "123123123", password, "user");

        var user = new User(signUp.Email, signUp.Username, passwordManager.Secure(password), "user", clock.Current(),
            CreatePatient(signUp));
   
        await _testDatabase.Context.Database.MigrateAsync();
        await _testDatabase.Context.Users.AddAsync(user);
        await _testDatabase.Context.SaveChangesAsync();

        // Act
        var command = new SignIn(user.Email, password);
        var response = await Client.PostAsJsonAsync("Users/sign-in", command);
        var jwt = await response.Content.ReadFromJsonAsync<JsonWebToken>();

        // Assert
        jwt.Should().NotBeNull();
        jwt.AccessToken.Should().NotBeNullOrWhiteSpace();
    }


    [Fact]
    public async Task GET_usersMe_UserAuthenticated_ReturnsUserBasicInfo()
    {
        // Arrange
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        var clock = new Clock();
        const string password = "secret";
        var signUp = new SignUp("test@lethimcookv2.com", "cooker", "first", "second", new DateTime(2000, 4, 1),
            "123123123", password, "user");

        var user = new User(signUp.Email, signUp.Username, passwordManager.Secure(password), "user", clock.Current(),
            CreatePatient(signUp));

        await _testDatabase.Context.Database.MigrateAsync();
        await _testDatabase.Context.Users.AddAsync(user);
        await _testDatabase.Context.SaveChangesAsync();

        // Act
        Authorize(user.Id, user.Role);
        var userDto = await Client.GetFromJsonAsync<UserDto>("users/me");

        // Assert
        userDto.Should().NotBeNull();
        userDto.Id.Should().Be(user.Id.Value);
    }

    private Patient CreatePatient(SignUp signUp)
        => new(signUp.FirstName, signUp.LastName, signUp.DateOfBirth, signUp.PhoneNumber);
}