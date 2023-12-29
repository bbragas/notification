using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notification.Api.Gateway.Http.Models;
using System.Text.Json;

namespace Notification.Api.Common.Extensions.Tests
{
    [TestClass]
    public class JsonExtensionsTests
    {

        private readonly QueueNotSentCountDto _queueObject = new QueueNotSentCountDto()
        {
            Stats = new StatsDto()
            {
                Sending = 10
            },
            Success = true
        };

        [TestMethod]
        public void JsonDeserialize_WithValidJson_ReturnsDeserializedObject()
        {
            var json = JsonSerializer.Serialize(_queueObject);

            var result = json.JsonDeserialize<QueueNotSentCountDto>();

            Assert.IsNotNull(result);
            Assert.AreEqual(_queueObject?.Stats?.Sending, result?.Stats?.Sending);
            Assert.AreEqual(_queueObject?.Success, result?.Success);
        }     
    }
}