namespace Candidate_Test_Task.Application.Common.Utilities;
public class RegularExpressions
{
    public static string PhoneNumberRegExpression = @"^(\+\d{1,3}[- ]?)?\d{10}$";
    public static string LinkedInProfileRegExpression = @"^(http(s)?:\/\/)?([\w]+\.)?linkedin\.com\/(pub|in|profile)";
    public static string GithubProfileRegExpression = @"^(http(s)?:\/\/)?([\w]+\.)?github\.com\/([A-Za-z0-9])";
}

public class SessionKeys
{
    public static string CandidatesCachedDB = "CandidatesCachedDB";
}