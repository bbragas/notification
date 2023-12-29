using FluentValidation;

namespace Notification.Api.Messages.BlacklistEntity.DTOs.Validators
{
    public class GetAllBlacklistDTOValidator : AbstractValidator<GetAllBlacklistDTO>
    {
        public GetAllBlacklistDTOValidator()
        {
            ValidatePage();
            ValidatePerPage();
        }
        private void ValidatePage()
            => When(req => req.Page.HasValue, () =>
                RuleFor(paging => paging.Page)
                    .InclusiveBetween(1, int.MaxValue)
                        .WithMessage(ValidationResource.PageCannotBeLessThanOne));

        private void ValidatePerPage()
            => When(req => req.PerPage.HasValue, () =>
                RuleFor(paging => paging.PerPage)
                    .InclusiveBetween(50, 100)
                        .WithMessage(ValidationResource.PerPageMustToBeWithinRange));
    }
}
