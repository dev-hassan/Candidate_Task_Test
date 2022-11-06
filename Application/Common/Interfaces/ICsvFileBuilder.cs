using Candidate_Test_Task.Application.CandidateProfile.Queries.GetProfiles;

namespace Candidate_Test_Task.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    Task<byte[]> BuildCandidateProfilesFile(IEnumerable<CandidateProfileDto> candidateProfiles);
    Task<IEnumerable<CandidateProfileDto>> GetCandidateProfiles();
    Task<CandidateProfileDto> GetCandidateProfileByEmail(string email);
}
