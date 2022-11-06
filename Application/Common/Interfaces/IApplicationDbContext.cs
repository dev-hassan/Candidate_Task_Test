using Candidate_Test_Task.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Candidate_Test_Task.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<CandidateApplication> CandidateApplications { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
