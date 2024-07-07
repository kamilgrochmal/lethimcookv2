using LetHimCookV2.API.Domain.Exceptions;

namespace LetHimCookV2.API.Application.Exceptions;

public class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}