using Candidate_Test_Task.Application.Common.Interfaces;

namespace Candidate_Test_Task.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
