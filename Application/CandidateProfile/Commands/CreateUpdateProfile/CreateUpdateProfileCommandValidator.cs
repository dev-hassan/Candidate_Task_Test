using Candidate_Test_Task.Application.Common.Utilities;
using FluentValidation;

namespace Candidate_Test_Task.Application.CandidateProfile.Commands.CreateUpdateProfile;
public class CreateUpdateProfileCommandValidator : AbstractValidator<CreateUpdateProfileCommand>
{
    public CreateUpdateProfileCommandValidator()
    {
        RuleFor(p => p.FirstName)
            .NotEmpty()
            .MaximumLength(25)
            .MinimumLength(3);

        RuleFor(p => p.LastName)
            .NotEmpty()
            .MaximumLength(25)
            .MinimumLength(3);

        RuleFor(p => p.PhoneNumber)
           .Matches(RegularExpressions.PhoneNumberRegExpression)
           .MaximumLength(15)
           .MinimumLength(4);

        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);

        RuleFor(p => p.LinkedInProfielUrl)
            .Matches(RegularExpressions.LinkedInProfileRegExpression);

        RuleFor(p => p.GithubProfielUrl)
            .Matches(RegularExpressions.GithubProfileRegExpression);

        RuleFor(p => p.Comment)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(500);
    }

    private static bool LinkMustBeAUri(string link)
    {
        if (string.IsNullOrWhiteSpace(link))
            return false;

        Uri outUri;
        return Uri.TryCreate(link, UriKind.Absolute, out outUri)
               && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
    }
}

