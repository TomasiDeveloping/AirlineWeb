using AirlineSendAgent.Dtos;

namespace AirlineSendAgent.Client
{
    public interface IWebhookClient
    {
        Task SendWebhookNotification(FlightDetailChangePayloadDto flightDetailChangePayload);
    }
}
