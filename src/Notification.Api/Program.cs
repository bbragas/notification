using Amazon.SQS;
using FluentValidation;
using FluentValidation.AspNetCore;
using Notification.Api.Commands;
using Notification.Api.Commands.Settings;
using Notification.Api.Infrastructure;
using Notification.Api.Infrastructure.Repository;
using Prometheus;
using Notification.Api.Messages.Abstractions.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Notification.Api.Configurations;
using Notification.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLog();

builder.Configuration.AddJsonFile("appsettings.json").AddEnvironmentVariables();

builder.Services.AddControllers();

var environmentSettings = new EnvironmentSettings();
builder.Configuration.GetSection("Environment").Bind(environmentSettings, c => c.BindNonPublicProperties = true);

builder.Services
    .AddEndpointsApiExplorer()
    .AddAWSService<IAmazonSQS>()
    .AddRepositoryServices()
    .AddComandAndQueryServices()
    .AddServices()
    .AddSettings(builder.Configuration)
    .AddHealthChecks(builder.Configuration)
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<IDataTransferObjectValidator>()
    .AddAutoMapper(typeof(DtoToCommandMappingConfiguration))
    .AddExceptionServices()
    .AddWhatsappServiceProvider(builder.Configuration)
    .RegisterAutoValidation();

    builder.Services.AddApiVersioning(o =>
    {
        o.AssumeDefaultVersionWhenUnspecified = true;
        o.DefaultApiVersion = new ApiVersion(1, 0);
    });
    builder.Services.AddVersionedApiExplorer(o =>
    {
        o.GroupNameFormat = "'v'VVV";
        o.SubstituteApiVersionInUrl = true;
    });
    builder.Services.AddSwaggerGen(o =>
    {
        o.OperationFilter<SwaggerDefaultValues>();
    });
    builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();

if (environmentSettings.HasSwagger)
    builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHealthCheck();
app.UseMiddleware<SwaggerHeadersMiddleware>();
app.UseMiddleware<MandatoryHeadersMiddleware>();
app.UseMiddleware<ScoppedLoggingMiddleware>();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (environmentSettings.HasSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMetricServer();

app.UseExceptionConfigure(app.Services);

app.Run();
