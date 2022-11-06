namespace Candidate_Test_Task.Domain.Entities;
public class CandidateApplication : BaseAuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public string LinkedInProfielUrl { get; set; }
    public string GithubProfielUrl { get; set; }
    public string Comment { get; set; }
}
