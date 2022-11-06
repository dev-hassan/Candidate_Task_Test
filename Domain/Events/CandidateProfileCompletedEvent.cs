namespace Candidate_Test_Task.Domain.Events;

public class CandidateProfileCompletedEvent : BaseEvent
{
    public CandidateProfileCompletedEvent(CandidateApplication candidateProfile)
    {
        CandidateApplication = candidateProfile;
    }

    public CandidateApplication CandidateApplication { get; }
}
