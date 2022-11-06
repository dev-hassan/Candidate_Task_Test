using Candidate_Test_Task.Application.CandidateProfile.Commands.CreateUpdateProfile;
using Candidate_Test_Task.Application.Common.Exceptions;
using Candidate_Test_Task.Application.Common.Models;
using Candidate_Test_Task.Domain.Entities;
using Candidate_Test_Task.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Candidate_Test_Task.Application.IntegrationTests.CandidateProfile.Commands;

using static Testing;

public class UpdateProfileTests : BaseTestFixture
{
    [Test]
    public async Task ShouldEmailCreatedBefore()
    {
        var command = new CreateUpdateProfileCommand {Email = "dev_m_hassan@hotmail.com"};
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<Exception>();
    }

    [Test]
    public async Task ShouldUpdateCandidateProfile()
    {
        var command = new CreateUpdateProfileCommand
        {
            FirstName = "Mahmoud",
            LastName = "Hassan",
            Email = "dev_m_hassan@hotmail.com",
            PhoneNumber = "+201008305188",
            ScheduledDateTime = DateTime.Now.AddDays(5),
            LinkedInProfielUrl = "https://www.linkedin.com/in/dev-hassan-2010/",
            GithubProfielUrl = "https://github.com/dev-hassan",
            Comment = "Hey sigma updates"
        };

        var result = await SendAsync(command);

        Assert.AreEqual(Result.Success().Succeeded, result.Succeeded);
    }
}
