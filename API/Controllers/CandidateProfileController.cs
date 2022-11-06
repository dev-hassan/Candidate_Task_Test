using Candidate_Test_Task.Application.CandidateProfile.Commands.CreateUpdateProfile;
using Candidate_Test_Task.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CandidateProfileController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Result>> CreateUpdate(CreateUpdateProfileCommand command)
    {
        return await Mediator.Send(command);
    }
}
