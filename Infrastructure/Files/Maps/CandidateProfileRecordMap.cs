using System.Globalization;
using Candidate_Test_Task.Application.CandidateProfile.Queries.GetProfiles;
using CsvHelper.Configuration;

namespace Candidate_Test_Task.Infrastructure.Files.Maps;

public class CandidateProfileRecordMap : ClassMap<CandidateProfileDto>
{
    public CandidateProfileRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(m => m.FirstName).Name("first_name");
        Map(m => m.LastName).Name("last_name");
        Map(m => m.Email).Name("email");
        Map(m => m.LinkedInProfielUrl).Name("linkedin_profile_url");
        Map(m => m.GithubProfielUrl).Name("github_profile_url");
        Map(m => m.PhoneNumber).Name("phone_number").ConvertUsing(c => string.IsNullOrEmpty(c.PhoneNumber) ? "NULL" : c.PhoneNumber);
        Map(m => m.ScheduledDateTime).Name("scheduled_time").ConvertUsing(c => c.ScheduledDateTime.Date > DateTime.MinValue ? c.ScheduledDateTime.ToString("MM/dd/yyyy hh:mm tt") : "NULL");
        Map(m => m.Comment).Name("comment").ConvertUsing(c => string.IsNullOrEmpty(c.Comment) ? "NULL" : c.Comment);
    }
}
