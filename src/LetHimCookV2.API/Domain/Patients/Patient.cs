using LetHimCookV2.API.Domain.Documents;
using LetHimCookV2.API.Domain.Users;

namespace LetHimCookV2.API.Domain.Patients;

public class Patient
{
    public PatientId PatientId { get; init; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    
    public UserId UserId { get; set; }
    public User User { get; set; }
    public ICollection<Document> Documents { get; set; }

    public Patient()
    {
        
    }
    public Patient(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
    }
}

public record PatientId(long Id);