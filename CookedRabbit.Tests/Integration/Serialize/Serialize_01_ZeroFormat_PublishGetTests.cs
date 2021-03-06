﻿using AutoFixture;
using CookedRabbit.Library.Models;
using CookedRabbit.Tests.Fixtures;
using CookedRabbit.Tests.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static CookedRabbit.Library.Utilities.Enums;

namespace CookedRabbit.Tests.Integration
{
    [Collection("IntegrationTests_NoCompression_ZeroFormat")]
    public class Serialize_01_ZeroFormat_PublishGetTests
    {
        private readonly ZeroFormat_NoCompression _fixture;

        public Serialize_01_ZeroFormat_PublishGetTests(ZeroFormat_NoCompression fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("Net Serialize", "ZeroFormat NoCompression PublishGet")]
        public async Task PublishExceptionAsync()
        {
            // Arrange
            var queueName = $"{_fixture.TestQueueName1}.3111";
            var exchangeName = string.Empty;

            var testObject = new TestHelperObject
            {
                Name = "RussianTestObject",
                Address = "1600 Pennsylvania Ave.",
                BitsAndPieces = new List<string> { "russianoperative", "spraytan", "traitor", "peetape", "kompromat" }
            };

            var envelope = new Envelope
            {
                ExchangeName = exchangeName,
                RoutingKey = queueName,
                ContentEncoding = ContentEncoding.Binary,
                MessageType = $"{ContentType.Textplain.Description()}{Charset.Utf8.Description()}"
            };

            // Act
            var createSuccess = await _fixture.RabbitTopologyService.QueueDeclareAsync(queueName);
            await Assert.ThrowsAsync<System.InvalidOperationException>(() => _fixture.RabbitSerializeService.SerializeAndPublishAsync(testObject, envelope));
            var deleteSuccess = await _fixture.RabbitTopologyService.QueueDeleteAsync(queueName);

            Assert.True(createSuccess, "Queue was not created.");
            Assert.True(deleteSuccess);
        }

        [Fact]
        [Trait("Net Serialize", "ZeroFormat NoCompression PublishGet")]
        public async Task PublishAndGetAsync()
        {
            // Arrange
            var queueName = $"{_fixture.TestQueueName2}.3112";
            var exchangeName = string.Empty;

            var testObject = new ZeroTestHelperObject
            {
                Name = "RussianTestObject",
                Address = "1600 Pennsylvania Ave.",
                BitsAndPieces = new List<string> { "russianoperative" , "spraytan", "traitor", "peetape", "kompromat" }
            };

            var envelope = new Envelope
            {
                ExchangeName = exchangeName,
                RoutingKey = queueName,
                ContentEncoding = ContentEncoding.Binary,
                MessageType = $"{ContentType.Textplain.Description()}{Charset.Utf8.Description()}"
            };

            // Act
            var createSuccess = await _fixture.RabbitTopologyService.QueueDeclareAsync(queueName);
            var publishSuccess = await _fixture.RabbitSerializeService.SerializeAndPublishAsync(testObject, envelope);

            await Task.Delay(200); // Allow Server Side Routing

            var result = await _fixture.RabbitSerializeService.GetAndDeserializeAsync<ZeroTestHelperObject>(queueName);
            var deleteSuccess = await _fixture.RabbitTopologyService.QueueDeleteAsync(queueName);

            // Assert
            Assert.True(createSuccess, "Queue was not created.");
            Assert.True(publishSuccess, "Message failed to publish.");
            Assert.True(result != null, "Result was null.");
            Assert.True(deleteSuccess);
        }

        [Fact]
        [Trait("Net Serialize", "ZeroFormat NoCompression PublishGet")]
        public async Task PublishManyAndGetManyAsync()
        {
            var fixture = new Fixture();

            // Arrange
            var messageCount = 17;
            var messages = fixture.CreateMany<ZeroTestHelperObject>(messageCount).ToList();
            var queueName = $"{_fixture.TestQueueName3}.3113";
            var exchangeName = string.Empty;
            var envelope = new Envelope
            {
                ExchangeName = exchangeName,
                RoutingKey = queueName,
                ContentEncoding = ContentEncoding.Binary,
                MessageType = $"{ContentType.Textplain.Description()}{Charset.Utf8.Description()}"
            };

            // Act
            var createSuccess = await _fixture.RabbitTopologyService.QueueDeclareAsync(queueName);
            var failures = await _fixture.RabbitSerializeService.SerializeAndPublishManyAsync(messages, envelope);

            await Task.Delay(200); // Allow Server Side Routing

            var results = await _fixture.RabbitSerializeService.GetAndDeserializeManyAsync<ZeroTestHelperObject>(queueName, messageCount);
            var deleteSuccess = await _fixture.RabbitTopologyService.QueueDeleteAsync(queueName);

            // Assert
            Assert.True(createSuccess, "Queue was not created.");
            Assert.Empty(failures);
            Assert.True(results.Count == messageCount, "Messages were lost.");
            Assert.True(deleteSuccess);
        }
    }
}
