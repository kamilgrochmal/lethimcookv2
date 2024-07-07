using System.Net;
using System.Net.Http.Headers;
using FluentAssertions;
using LetHimCookV2.API.Application.Requests;
using LetHimCookV2.API.Domain.Patients;
using LetHimCookV2.API.Domain.Users;
using LetHimCookV2.API.Infrastructure.Security;
using LetHimCookV2.API.Infrastructure.Time;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LetHimCookV2.API.Tests.API;

public class DocumentsTests : ControllerTests, IDisposable
{
    private readonly TestDatabase _testDatabase;

    public DocumentsTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }

    /// <summary>
    /// Uploads real pdf file and checks if it was uploaded correctly
    /// </summary>
    [Fact]
    public async Task POST_Documents_ValidData_UploadsDocumentAndReturns200()
    {
        //Arrange
        await SetupUser();
        var userId = 1;
        Authorize(userId, "user");
        var request = await PreparePostRequestWithPdfFile();

        //Act
        var response = await Client.SendAsync(request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var document = await _testDatabase
            .Context
            .Documents
            .Include(document => document.Patient)
            .ThenInclude(patient => patient.User)
            .SingleOrDefaultAsync();
        document.Patient.User.Id.Value.Should().Be(userId);
    }

    private async Task SetupUser()
    {
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
    }

    private async Task<HttpRequestMessage> PreparePostRequestWithPdfFile()
    {
        var filePath = "czlowiek-komputer.pdf";
        var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            Name = "File",
            FileName = "czlowiek-komputer.pdf"
        };
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

        var content = new MultipartFormDataContent();
        content.Add(fileContent);
        content.Add(new StringContent("Test Document"), "Title");
        content.Add(new StringContent("Test Description"), "Description");
        content.Add(new StringContent("1"), "CatalogId");
        content.Add(new StringContent(DateTime.Now.ToString("o")), "DocumentDate");

        var request = new HttpRequestMessage(HttpMethod.Post, "Documents")
        {
            Content = content
        };

        return request;
    }

    private Patient CreatePatient(SignUp signUp)
        => new(signUp.FirstName, signUp.LastName, signUp.DateOfBirth, signUp.PhoneNumber);
}