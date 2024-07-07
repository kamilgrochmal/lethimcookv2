using LetHimCookV2.API.Domain.Patients;

namespace LetHimCookV2.API.Domain.Repositories;

public interface IPatientRepository
{
    Task<long> AddPatient(Patient patient);
    Task<Patient> GetPatient(PatientId patientId);
    Task UpdatePatient(Patient patient);
    Task DeletePatient(PatientId patientId);
}