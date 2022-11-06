using Candidate_Test_Task.Application.CandidateProfile.Commands.CreateUpdateProfile;
using Candidate_Test_Task.Application.CandidateProfile.Queries.GetProfiles;
using Candidate_Test_Task.Application.Common.Exceptions;
using Candidate_Test_Task.Application.Common.Interfaces;
using Candidate_Test_Task.Application.Common.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Candidate_Test_Task.Application.IntegrationTests.CandidateProfile.Commands;

using static Testing;

public class CreateProfileTests : BaseTestFixture
{
    [Test]
    public async Task ShouldThrowValidationErrorWhenNotSendRequiredFields()
    {
        var command = new CreateUpdateProfileCommand { FirstName = "test", LastName ="test2" };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateCandidateProfile()
    {
        var candidateProfile = new CreateUpdateProfileCommand
        {
            FirstName = "Mahmoud",
            LastName = "Hassan",
            Email = "dev_m_hassan@hotmail.com",
            PhoneNumber = "+201008305188",
            ScheduledDateTime = DateTime.Today.AddDays(5),
            LinkedInProfielUrl = "https://www.linkedin.com/in/dev-hassan-2010/",
            GithubProfielUrl = "https://github.com/dev_hassan",
            Comment = "Heeeeeeeeey sigma",
        };


        var result = await SendAsync(candidateProfile);
        Assert.AreEqual(Result.Success().Succeeded, result.Succeeded);
    }

    [Test]
    public async Task ShouldThrowExceptionWhenSendInvalidEmail()
    {
        var candidateProfile = new CreateUpdateProfileCommand
        {
            FirstName = "Mahmoud",
            LastName = "Hassan",
            Email = "dev_m_hassan++hotmail.com",
            PhoneNumber = "+201008305188",
            ScheduledDateTime = DateTime.Today.AddDays(5),
            LinkedInProfielUrl = "https://www.linkedin.com/in/dev-hassan-2010/",
            GithubProfielUrl = "https://github.com/dev_hassan",
            Comment = "Heeeeeeeeey sigma",
        };

        Assert.ThrowsAsync<ValidationException>(() =>   SendAsync(candidateProfile));
    }
}
