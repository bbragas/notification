using FluentValidation;

namespace Notification.Api.Models.v1.Email.Validators;

public class NotifyEmailDTOValidator : AbstractValidator<NotifyEmailDTO>
{
    public NotifyEmailDTOValidator()
    {
        RuleFor(dto => dto.Body).NotEmpty();
        RuleFor(dto => dto.Client).NotEmpty();
        RuleFor(dto => dto.Subject).NotEmpty();
        RuleFor(dto => dto.Campaign).NotEmpty();
        RuleFor(dto => dto.ProjectId).NotEmpty();
        RuleFor(dto => dto.SenderName).NotEmpty();
        RuleFor(dto => dto.RecipientName).NotEmpty();
        RuleFor(dto => dto.ProjectEntityId).NotEmpty();
        RuleFor(dto => dto.SenderEmail).NotEmpty().EmailAddress();
        RuleFor(dto => dto.RecipientEmail).NotEmpty().EmailAddress();
    }
}
