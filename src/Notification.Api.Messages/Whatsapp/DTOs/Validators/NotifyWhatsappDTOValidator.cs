using FluentValidation;

namespace Notification.Api.Messages.Whatsapp.DTOs.Validators;

public class NotifyWhatsappDTOValidator : AbstractValidator<NotifyWhatsappRequestDTO>
{
    public NotifyWhatsappDTOValidator()
    {
        ValidateUnitId();
        ValidateProductId();
        ValidateProductName();
        ValidateMessage();
        ValidateRecipientNumber();
        ValidateMessageType();
    }
    private void ValidateUnitId()
            => RuleFor(notification => notification.UnitId)
                .NotEqual(Guid.Empty)
                    .WithMessage(ValidationResource.UnitIdInvalid);

    private void ValidateProductId()
           => RuleFor(notification => notification.ProjecttId)
               .NotEqual(Guid.Empty)
                   .WithMessage(ValidationResource.ProductIdInvalid);

    private void ValidateProductName()
           => RuleFor(notification => notification.ProjectName)
               .NotNull()
                   .WithMessage(ValidationResource.ProductNameInvalid)
                .NotEmpty()
                   .WithMessage(ValidationResource.ProductNameInvalid);

    private void ValidateMessage()
           => RuleFor(notification => notification.Message)
               .NotNull()
                   .WithMessage(ValidationResource.MessageInvalid)
                .NotEmpty()
                   .WithMessage(ValidationResource.MessageInvalid);

    private void ValidateRecipientNumber()
           => RuleFor(notification => notification.RecipientNumber)
               .NotNull()
                   .WithMessage(ValidationResource.RecipientNumberInvalid)
                .NotEmpty()
                   .WithMessage(ValidationResource.RecipientNumberInvalid);

    private void ValidateMessageType()
           => RuleFor(notification => notification.MessageType.GetHashCode())
               .InclusiveBetween(0, 3)
                   .WithMessage(ValidationResource.MessageTypeInvalid);
}
