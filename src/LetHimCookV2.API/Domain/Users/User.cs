using LetHimCookV2.API.Domain.Patients;

namespace LetHimCookV2.API.Domain.Users;

public class User
{
    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public Patient Patient { get; set; }

    public User()
    {
        
    }
    public User(Email email, Username username, Password password, Role role,
        DateTime createdAt, Patient patient)
    {
        Email = email;
        Username = username;
        Password = password;
        Role = role;
        CreatedAt = createdAt;
        Patient = patient;
    }
}