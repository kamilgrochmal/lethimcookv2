using LetHimCookV2.API.Application.DTO;
using LetHimCookV2.API.Application.Exceptions;
using LetHimCookV2.API.Application.Requests;
using LetHimCookV2.API.Application.Security;
using LetHimCookV2.API.Domain.Abstractions;
using LetHimCookV2.API.Domain.Patients;
using LetHimCookV2.API.Domain.Repositories;
using LetHimCookV2.API.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace LetHimCookV2.API.Infrastructure.Auth;

internal class IdentityService : IIdentityService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IAuthManager _authManager;
    private readonly IClock _clock;

    public IdentityService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,
        IAuthManager authManager, IClock clock)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _authManager = authManager;
        _clock = clock;
    }

    public async Task<UserDto> GetAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(new UserId(id));

        return user is null
            ? null
            : new UserDto()
            {
                Id = user.Id,
                Username = user.Username
            };
    }

    public async Task<JsonWebToken> SignInAsync(SignIn dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email.ToLowerInvariant());
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        if (_passwordHasher.VerifyHashedPassword(default, user.Password, dto.Password) ==
            PasswordVerificationResult.Failed)
        {
            throw new InvalidCredentialsException();
        }

        var jwt = _authManager.CreateToken(user.Id.Value.ToString(), user.Role);
        jwt.Email = user.Email;

        return jwt;
    }

    public async Task SignUpAsync(SignUp signUp)
    {
        var email = new Email(signUp.Email);
        var username = new Username(signUp.Username);
        var role = string.IsNullOrWhiteSpace(signUp.Role) ? Role.User() : new Role(signUp.Role);

        if (await _userRepository.GetByEmailAsync(email) is not null)
        {
            throw new EmailAlreadyInUseException(email);
        }

        if (await _userRepository.GetByUsernameAsync(username) is not null)
        {
            throw new UsernameAlreadyInUseException(username);
        }

        var securedPassword = _passwordHasher.HashPassword(default, signUp.Password);
        var user = new User(email, username, securedPassword, role, _clock.Current(), CreatePatient(signUp));
        await _userRepository.AddAsync(user);
    }

    private Patient CreatePatient(SignUp signUp)
        => new(signUp.FirstName, signUp.LastName, signUp.DateOfBirth, signUp.PhoneNumber);
}