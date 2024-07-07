using LetHimCookV2.API.Domain.Patients;

namespace LetHimCookV2.API.Application.DTO;

public record PatientDto
{
    public long Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTime DateOfBirth { get; init; }
    
    private PatientDto(long id, string firstName, string lastName, DateTime dateOfBirth)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }
    
    public static PatientDto ToDto(Patient patient) =>
        new(patient.PatientId.Id, patient.FirstName, patient.LastName, patient.DateOfBirth);
}