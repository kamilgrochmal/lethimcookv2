using LetHimCookV2.API.Application.DTO;
using LetHimCookV2.API.Application.Services;

namespace LetHimCookV2.API.Infrastructure.Services;

public class PatientService : IPatientService
{
    public PatientService()
    {
        
    }
    public Task<PatientDto> GetPatient(long patientId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PatientDto>> GetPatients(long patientId)
    {
        throw new NotImplementedException();
    }
}