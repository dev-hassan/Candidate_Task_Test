using System.Globalization;
using System.Text;
using Candidate_Test_Task.Application.CandidateProfile.Queries.GetProfiles;
using Candidate_Test_Task.Application.Common.Interfaces;
using Candidate_Test_Task.Infrastructure.Files.Maps;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;

namespace Candidate_Test_Task.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    private readonly IConfiguration _configuration;

    public CsvFileBuilder(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<byte[]> BuildCandidateProfilesFile(IEnumerable<CandidateProfileDto> candidateProfiles)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Configuration.RegisterClassMap<CandidateProfileRecordMap>();
            await csvWriter.WriteRecordsAsync(candidateProfiles);
        }

        return memoryStream.ToArray();
    }


    public async Task<IEnumerable<CandidateProfileDto>> GetCandidateProfiles()
    {
        var candidateProfiles = new List<CandidateProfileDto>();
        var csvFilePath =Path.Combine(Directory.GetCurrentDirectory(), _configuration.GetValue<string>("csvFilePath"));
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8, // Our file uses UTF-8 encoding.
            Delimiter = ",",// The delimiter is a comma.
        };

        using (var fs = File.Open(csvFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (var textReader = new StreamReader(fs, Encoding.UTF8))
            using (var csv = new CsvReader(textReader, configuration))
            {
                csv.Configuration.RegisterClassMap<CandidateProfileRecordMap>();
                var data = csv.GetRecordsAsync<CandidateProfileDto>();

                await foreach (var profileDto in data)
                {
                    candidateProfiles.Add(profileDto);
                }
            }
        }
        return candidateProfiles;
    }

    public async Task<CandidateProfileDto> GetCandidateProfileByEmail(string email)
    {
        throw new NotImplementedException();
    }
}
