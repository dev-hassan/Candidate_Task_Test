namespace Candidate_Test_Task.Domain.Events;

public class CandidateProfileCreatedEvent : BaseEvent
{
    public CandidateProfileCreatedEvent(CandidateApplication candidateProfile)
    {
        CandidateProfile = candidateProfile;
    }

    public CandidateApplication CandidateProfile { get; }
}
