using BasicTopicSubscriber.Core.DTOs;
using BasicTopicSubscriber.Core.ExternalServices;
using NSubstitute;
using NUnit.Framework;
using System.Net;
using System.Text;
using System.Text.Json;

namespace BasicTopicSubscriber.Tests.ExternalServices
{
    [TestFixture]
    public class EmailFacadeTests
    {
        private IHttpClientFactory _httpClientFactory;
        
        private EmailFacade _sut;

        [SetUp]
        public void SetUp()
        {
            _httpClientFactory = Substitute.For<IHttpClientFactory>();

            _sut = new EmailFacade(_httpClientFactory);
        }

        [Test]
        public void SendAsync_WithNullEmailRequest_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _sut.SendAsync(null));
        }

        [Test]
        public async Task SendAsync_WithValidRequestAndErrorResponse_ReturnsFailureResult()
        {
            // Arrange
            var emailRequest = new EmailRequest("to", "subject", "body");

            var errorResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Error message", Encoding.UTF8, "application/json")
            };

            using var httpTestServer = new TestHttpMessageHandler(errorResponse);

            using var httpClient = new HttpClient(httpTestServer) 
            { 
                BaseAddress = new Uri("http://localhost") 
            };

            _httpClientFactory.CreateClient("external-service")
                              .Returns(httpClient);

            // Act
            var result = await _sut.SendAsync(emailRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.IsSuccess);
                Assert.AreEqual("Error message", result.ErrorMessage);
            });
        }

        [Test]
        public async Task SendAsync_WithValidRequestAndSuccessResponse_ReturnsSuccessResult()
        {
            // Arrange
            var emailRequest = new EmailRequest("to", "subject", "body");
            var emailResponse = new EmailResponse(Guid.NewGuid(), DateTime.Now.AddMinutes(-1));

            var successResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(emailResponse), Encoding.UTF8, "application/json")
            };

            using var httpTestServer = new TestHttpMessageHandler(successResponse);

            using var httpClient = new HttpClient(httpTestServer) 
            { 
                BaseAddress = new Uri("http://localhost") 
            };

            _httpClientFactory.CreateClient("external-service")
                              .Returns(httpClient);

            // Act
            var result = await _sut.SendAsync(emailRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsSuccess);
                Assert.AreEqual(emailResponse, result.Value);
            });
        }
    }
}
