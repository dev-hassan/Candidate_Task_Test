namespace Candidate_Test_Task.Domain.Events;

public class CandidateProfileUpdatedEvent : BaseEvent
{
    public CandidateProfileUpdatedEvent(CandidateApplication candidateProfile)
    {
        CandidateProfile = candidateProfile;
    }

    public CandidateApplication CandidateProfile { get; }
}
