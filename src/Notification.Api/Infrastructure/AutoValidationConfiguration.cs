using FluentValidation;
using FluentValidation.AspNetCore;
using Notification.Api.Models.v1.Email;
using Notification.Api.Models.v1.Email.Validators;
using Notification.Api.Models.v1.Sms;
using Notification.Api.Models.v1.Sms.Validators;

namespace Notification.Api.Infrastructure;

public static class AutoValidationConfiguration
{
    public static IServiceCollection RegisterAutoValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddScoped<IValidator<NotifySmsDTO>, NotifySmsDTOValidator>();
        services.AddScoped<IValidator<NotifyEmailDTO>, NotifyEmailDTOValidator>();
        services.AddScoped<IValidator<NotifyEmailBatchCustomerDTO>, NotifyEmailBatchCustomerDTOValidator>();

        return services;
    }
}
