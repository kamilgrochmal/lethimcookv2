using LetHimCookV2.API.Domain.Patients;

namespace LetHimCookV2.API.Application.Requests;

public record SignUp(
    string Email,
    string Username,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string PhoneNumber,
    string Password,
    string Role);