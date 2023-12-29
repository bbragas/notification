using FluentValidation;

namespace Notification.Api.Messages.BlacklistEntity.DTOs.Validators
{
    public class BlacklistItemRequestDTOValidator : AbstractValidator<BlacklistItemRequestDTO> 
    {
        public BlacklistItemRequestDTOValidator()
        {
            ValidateProjectId();
            ValidateContact();
        }

        private void ValidateProjectId()
            => RuleFor(projectId => projectId.ProjectId)
                .NotEqual(Guid.Empty)
                    .WithMessage(ValidationResource.ProjectIdCannotBeEmpty);

            private void ValidateContact()
            => RuleFor(contact => contact.Contact)
                .NotNull()
                    .WithMessage(ValidationResource.ContactCannotBeNullOrEmpty)
                .NotEmpty()
                    .WithMessage(ValidationResource.ContactCannotBeNullOrEmpty);
    }
    
}
