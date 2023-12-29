using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Notification.Api.Gateway.Http.Models;
using Notification.Api.Gateway.Http.Whatsapp;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Notification.Api.Gateway.Http.Tests.WhatsappClient
{
    [TestClass]
    public class WhenCallWhatsappHttpClientSuccess
    {
        private readonly HttpClient _httpClient;
        private readonly Mock<IOptions<WhatsappProviderSettings>> _whatsappServiceSettings;
        private readonly WhatsappHttpClient _client;
        private readonly Uri _uri = new("https://api.maytapi.com/api/");
        private readonly QueueNotSentCountDto _response = new QueueNotSentCountDto() { Success = true, Stats = new() { Sending = 10 } };
        private readonly WhatsappProviderSettings _providerSettings = new WhatsappProviderSettings() { PhoneId = "26162", ProductId = Guid.NewGuid() };

        public WhenCallWhatsappHttpClientSuccess()
        {
            _whatsappServiceSettings = new Mock<IOptions<WhatsappProviderSettings>>();
            _whatsappServiceSettings
                .Setup(s => s.Value).Returns(_providerSettings);

            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("")
                .Respond("application/json", JsonSerializer.Serialize(_response));

            _httpClient = new HttpClient(mockHttp);
            _httpClient.BaseAddress = _uri;
            _client = new WhatsappHttpClient(_httpClient, _whatsappServiceSettings.Object);
        }

        [TestMethod]
        public async Task Should_Return_Not_Sent_Messages_Count_Correctly()
        {
            var response = await _client.GetQueueNotSendCountAsync(It.IsAny<CancellationToken>());

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Stats?.Sending, _response.Stats?.Sending);
            Assert.IsTrue(response.Success);
        }
    }
}
