using AutoMapper;
using Notification.Api.Infrastructure;
using Notification.Api.Models.v1.Email;
using Notification.Api.Models.v1.Email.Validators;

namespace Notification.Api.MappingProfiles.Tests;

[TestClass]
public class DtoToCommandMappingProfileTests
{
    [TestMethod]
    public void CreateMap_Should_Map_DtoToCommand()
    {
        var config = new MapperConfiguration((cfg) =>
            cfg.AddProfile<DtoToCommandMappingConfiguration>());

        var validationResult = new NotifyEmailBatchCustomerDTOValidator().Validate(new NotifyEmailBatchCustomerDTO());


        config.AssertConfigurationIsValid();
    }
}