using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Notification.Api.Gateway.Http.Models;
using Notification.Api.Gateway.Http.Whatsapp;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Notification.Api.Gateway.Http.Tests.WhatsappClient
{
    [TestClass]
    public class WhenCallWhatsappHttpClientFailed
    {
        private readonly HttpClient _httpClient;
        private readonly Mock<IOptions<WhatsappProviderSettings>> _whatsappServiceSettings;
        private readonly WhatsappHttpClient _client;
        private readonly Uri _uri = new("https://api.maytapi.com/api/");
        private readonly WhatsappProviderSettings _providerSettings = new WhatsappProviderSettings() { PhoneId = "26162", ProductId = Guid.NewGuid() };


        public WhenCallWhatsappHttpClientFailed()
        {
            _whatsappServiceSettings = new Mock<IOptions<WhatsappProviderSettings>>();
            _whatsappServiceSettings
                .Setup(s => s.Value).Returns(_providerSettings);

            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("").Respond(System.Net.HttpStatusCode.NotFound);

            _httpClient = new HttpClient(mockHttp);
            _httpClient.BaseAddress = _uri;
            _client = new WhatsappHttpClient(_httpClient, _whatsappServiceSettings.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public Task Should_Return_Not_Sent_Messages_Count_Exception()
            => _client.GetQueueNotSendCountAsync(It.IsAny<CancellationToken>());

    }
}
