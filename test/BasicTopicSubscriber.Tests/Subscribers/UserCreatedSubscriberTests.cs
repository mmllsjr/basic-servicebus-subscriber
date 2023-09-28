using Azure.Messaging.ServiceBus;
using BasicTopicSubscriber.Core.Entities;
using BasicTopicSubscriber.Core.Interfaces;
using BasicTopicSubscriber.Subscribers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System.Text.Json;

namespace BasicTopicSubscriber.Tests.Subscribers
{
    //[TestFixture]
    //public class UserCreatedSubscriberTests
    //{
    //    private ILogger<UserCreatedSubscriber> _logger;
    //    private IUserService _userService;
    //    private ServiceBusClient _serviceBusClient;

    //    private UserCreatedSubscriber _sut;

    //    [SetUp]
    //    public void SetUp()
    //    {
    //        _logger = Substitute.For<ILogger<UserCreatedSubscriber>>();
    //        _userService = Substitute.For<IUserService>();
    //        _serviceBusClient = Substitute.For<ServiceBusClient>("your-connection-string");

    //        _sut = new UserCreatedSubscriber(_serviceBusClient, _logger, _userService);
    //    }

    //    [Test]
    //    public async Task ProcessMessageAsync_ShouldCallUserService()
    //    {
    //        // Arrange
    //        var user = new User { Id = 1, Name = "Test User", Email = "test@example.com" };
    //        var receiver = Substitute.For<ServiceBusReceiver>(Substitute.For<ServiceBusConnection>(), "your-entity-path");
    //        var receivedMessage = ServiceBusModelFactory.ServiceBusReceivedMessage
    //            (
    //                body: BinaryData.FromString(JsonSerializer.Serialize(user))
    //            );

    //        var args = new ProcessMessageEventArgs(receivedMessage, _ => Task.CompletedTask, Substitute.For<ServiceBusReceiver>(), CancellationToken.None);

    //        // Act
    //        await _sut.ProcessMessageAsync(args);

    //        // Assert
    //        await _userService.Received().ApplySomethingAndSendEmailAsync(user);
    //        _logger.Received().LogInformation("Starting some processing for UserId {userId}", user.Id);
    //        _logger.Received().LogInformation("UserId {userId} processed", user.Id);
    //    }

    //    [Test]
    //    public async Task HandleErrorAsync_ShouldLogError()
    //    {
    //        // Arrange
    //        var exception = new Exception("Test exception");
    //        var args = new ProcessErrorEventArgs("your-entity-path", exception, _ => Task.CompletedTask, CancellationToken.None);

    //        // Act
    //        await _sut.HandleErrorAsync(args);

    //        // Assert
    //        _logger.Received().LogInformation("Starting some error handling for UserCreatedSubscriber");
    //        _logger.Received().LogError(exception, "You can log the exception like this. Here's the exception message: {message}", exception.Message);
    //    }
    //}
}
