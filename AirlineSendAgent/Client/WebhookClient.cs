using System.Net.Http.Headers;
using System.Text.Json;
using AirlineSendAgent.Dtos;

namespace AirlineSendAgent.Client
{
    public class WebhookClient : IWebhookClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WebhookClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task SendWebhookNotification(FlightDetailChangePayloadDto flightDetailChangePayload)
        {
            var serializedPayload = JsonSerializer.Serialize(flightDetailChangePayload);

            var httpClient = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, flightDetailChangePayload.WebhookURI);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(serializedPayload);

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                using var response = await httpClient.SendAsync(request);
                Console.WriteLine("Success");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
