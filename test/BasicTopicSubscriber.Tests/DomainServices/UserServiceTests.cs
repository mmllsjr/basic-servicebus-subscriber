using BasicTopicSubscriber.Core.DomainServices;
using BasicTopicSubscriber.Core.DTOs;
using BasicTopicSubscriber.Core.Entities;
using BasicTopicSubscriber.Core.Exceptions;
using BasicTopicSubscriber.Core.Interfaces;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace BasicTopicSubscriber.Tests.DomainServices
{
    [TestFixture]
    public class UserServiceTests
    {
        private IEmailFacade _emailFacade;
        private ILogger<UserService> _logger;

        private IUserService _sut;

        [SetUp]
        public void SetUp()
        {
            _emailFacade = Substitute.For<IEmailFacade>();
            _logger = Substitute.For<ILogger<UserService>>();
            _sut = new UserService(_logger, _emailFacade);
        }

        [Test]
        public void ApplySomethingAndSendEmailAsync_WhenEmailSendingFails_ThrowsEmailSendingFailedException()
        {
            // Arrange
            var user = new User
            {
                Name = "Test User",
                Email = "test@example.com"
            };

            var errorMessage = "Email sending failed";

            _emailFacade.SendAsync(Arg.Any<EmailRequest>())
                        .Returns(Result<EmailResponse>.Failure(errorMessage));

            // Act & Assert
            var ex = Assert.ThrowsAsync<EmailSendingFailedException>(() => _sut.ApplySomethingAndSendEmailAsync(user));

            Assert.AreEqual($"Couldn't send e-mail to user. Reason: {errorMessage}", ex.Message);
        }

        [Test]
        public async Task ApplySomethingAndSendEmailAsync_WhenEmailSendingSucceeds_LogsInformation()
        {
            // Arrange
            var user = new User
            {
                Name = "Test User",
                Email = "test@example.com"
            };

            var createdAt = DateTime.Now.AddMinutes(-1);
            var createdEmailId = Guid.NewGuid();

            var emailResponse = new EmailResponse(createdEmailId, createdAt);

            _emailFacade.SendAsync(Arg.Any<EmailRequest>())
                        .Returns(Result<EmailResponse>.Success(emailResponse));

            // Act
            await _sut.ApplySomethingAndSendEmailAsync(user);

            // Assert
            _logger.Received(1).Log(
                LogLevel.Information,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == $"E-mail created and sent with id {createdEmailId}"),
                Arg.Any<Exception>(),
                Arg.Any<Func<object, Exception, string>>());
        }
    }
}
