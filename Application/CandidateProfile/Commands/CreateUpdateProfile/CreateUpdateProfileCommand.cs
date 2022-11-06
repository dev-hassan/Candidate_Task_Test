using Candidate_Test_Task.Application.CandidateProfile.Queries.GetProfiles;
using Candidate_Test_Task.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Candidate_Test_Task.Application.CandidateProfile.Commands.CreateUpdateProfile;
public record CreateUpdateProfileCommand : IRequest<bool>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PhoneNumber { get; init; }
    public string Email { get; init; }
    public DateTime ScheduledDateTime { get; init; }
    public string LinkedInProfielUrl { get; init; }
    public string GithubProfielUrl { get; init; }
    public string Comment { get; init; }
}

public class CreateUpdateProfileCommandHandler : IRequestHandler<CreateUpdateProfileCommand, bool>
{
    private readonly ICsvFileBuilder _csvFileBuilder;
    private readonly IConfiguration _configuration;
    private ILogger<CreateUpdateProfileCommandHandler> _logger;

    public CreateUpdateProfileCommandHandler(ICsvFileBuilder csvFileBuilder, IConfiguration configuration, ILogger<CreateUpdateProfileCommandHandler> logger)
    {
        _csvFileBuilder = csvFileBuilder;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<bool> Handle(CreateUpdateProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //Get candidate profiles from cache instead of load csv file itself,
            //recomended to use redis, not implemented due to task time

            // read csv file 
            var candidateProfiles = await _csvFileBuilder.GetCandidateProfiles();

            // search by email
            var profile = candidateProfiles.FirstOrDefault(p => p.Email == request.Email);

            var requestProfile = new CandidateProfileDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Comment = request.Comment,
                GithubProfielUrl = request.GithubProfielUrl,
                LinkedInProfielUrl = request.LinkedInProfielUrl,
                ScheduledDateTime = request.ScheduledDateTime,
            };

            // Profile not exist, add new one else update existing profile
            if (profile == null)
                candidateProfiles = candidateProfiles.Append(requestProfile);
            else
            {
                profile.FirstName = request.FirstName;
                profile.LastName = request.LastName;
                profile.PhoneNumber = request.PhoneNumber;
                profile.Comment = request.Comment;
                profile.GithubProfielUrl = request.GithubProfielUrl;
                profile.LinkedInProfielUrl = request.LinkedInProfielUrl;
                profile.ScheduledDateTime = request.ScheduledDateTime;
            }

            //prepare csv file
            var fileAsBytes = await _csvFileBuilder.BuildCandidateProfilesFile(candidateProfiles);

            //update File
            var csvFilePath = _configuration["csvFilePath"];
            await File.WriteAllBytesAsync(csvFilePath, fileAsBytes, cancellationToken);

            // caching candidate profiles
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
            return false;
        }
        return true;
    }
}