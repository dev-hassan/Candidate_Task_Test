using Candidate_Test_Task.Application.CandidateProfile.Commands.CreateUpdateProfile;
using Candidate_Test_Task.Application.Common.Behaviours;
using Candidate_Test_Task.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Candidate_Test_Task.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateUpdateProfileCommand>> _logger = null!;
    private Mock<ICurrentUserService> _currentUserService = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateUpdateProfileCommand>>();
        _currentUserService = new Mock<ICurrentUserService>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUserService.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateUpdateProfileCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateUpdateProfileCommand 
        {
            FirstName = "Mahmoud",
            LastName = "Hassan",
            Email = "dev_m_hassan@hotmail.com",
            PhoneNumber = "+201008305188",
            ScheduledDateTime = DateTime.Now.AddDays(5),
            LinkedInProfielUrl = "https://www.linkedin.com/in/dev-hassan-2010/",
            GithubProfielUrl = "https://github.com/dev-hassan",
            Comment ="Hey sigma"
        }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateUpdateProfileCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateUpdateProfileCommand
        {
            FirstName = "Mahmoud",
            LastName = "Hassan",
            Email = "dev_m_hassan@hotmail.com",
            PhoneNumber = "+201008305188",
            ScheduledDateTime = DateTime.Now.AddDays(5),
            LinkedInProfielUrl = "https://www.linkedin.com/in/dev-hassan-2010/",
            GithubProfielUrl = "https://github.com/dev-hassan",
            Comment = "Hey sigma"
        }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
