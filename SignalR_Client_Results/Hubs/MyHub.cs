using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Primitives;

namespace SignalR_Client_Results.Hubs
{
    public interface IMyHub
    {
        Task<string> ReceiveMessage(string message);
    }
    public class MyHub : Hub<IMyHub>
    {
        public async Task SendMessageAsync(string message, string connectionId)
        {
            //string logMessage = await Clients.Client(connectionId).InvokeAsync<string>("receiveMessage", message, new());

            //Console.WriteLine(logMessage);


            string logMessage = await Clients.Client(connectionId).ReceiveMessage(message);

            Console.WriteLine(logMessage);
        }

        public async Task LogAsync(string logMessage)
        {
            Console.WriteLine(logMessage);
        }
    }
}
