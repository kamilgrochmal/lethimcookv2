using LetHimCookV2.API.Application.DTO;

namespace LetHimCookV2.API.Application.Services;

public interface IPatientService
{
    Task<PatientDto> GetPatient(long patientId);
    Task<IEnumerable<PatientDto>> GetPatients(long patientId);
}