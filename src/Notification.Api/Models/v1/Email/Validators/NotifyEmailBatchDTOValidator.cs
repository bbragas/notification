using FluentValidation;
using Notification.Api.Models.v1.Email;

namespace Notification.Api.Models.v1.Email.Validators;

public class NotifyEmailBatchCustomerDTOValidator : AbstractValidator<NotifyEmailBatchCustomerDTO>
{
    public NotifyEmailBatchCustomerDTOValidator()
    {
        RuleFor(dto => dto.Attributes).NotEmpty();
        RuleFor(dto => dto.RecipientName).NotEmpty();
        RuleFor(dto => dto.ProjectEntityId).NotEmpty();
        RuleFor(dto => dto.RecipientEmail).NotEmpty().EmailAddress();
    }
}

public class NotifyEmailBatchDTOValidator : AbstractValidator<NotifyEmailBatchDTO>
{
    public NotifyEmailBatchDTOValidator()
    {
        RuleFor(dto => dto.Body).NotEmpty();
        RuleFor(dto => dto.Client).NotEmpty();
        RuleFor(dto => dto.Subject).NotEmpty();
        RuleFor(dto => dto.Campaign).NotEmpty();
        RuleFor(dto => dto.ProjectId).NotEmpty();
        RuleFor(dto => dto.SenderName).NotEmpty();
        RuleFor(dto => dto.SenderEmail).NotEmpty().EmailAddress();
        RuleForEach(dto => dto.Customers).SetValidator(new NotifyEmailBatchCustomerDTOValidator());
    }
}
