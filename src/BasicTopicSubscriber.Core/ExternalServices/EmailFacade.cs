using BasicTopicSubscriber.Core.DTOs;
using BasicTopicSubscriber.Core.Interfaces;
using System.Text;
using System.Text.Json;

namespace BasicTopicSubscriber.Core.ExternalServices
{
    public class EmailFacade : IEmailFacade
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EmailFacade(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<EmailResponse>> SendAsync(EmailRequest emailRequest)
        {
            if(emailRequest == null)
            {
                throw new ArgumentNullException(nameof(emailRequest));
            }

            var client = _httpClientFactory.CreateClient("external-service");

            using var content = new StringContent
                (
                    JsonSerializer.Serialize(emailRequest), Encoding.UTF8, "application/json"
                );

            using var response = await client.PostAsync("email", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return Result<EmailResponse>.Failure(responseContent);
            }

            var emailResponse = JsonSerializer.Deserialize<EmailResponse>(responseContent);

            return Result<EmailResponse>.Success(emailResponse);
        }
    }
}
