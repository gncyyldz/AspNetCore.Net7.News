using Microsoft.AspNetCore.SignalR.Client;

HubConnection connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7286/myhub")
    .Build();

await connection.StartAsync();

Console.WriteLine($"ConnectionID : {connection.ConnectionId}");

connection.On<string, string>("receiveMessage", async message =>
{
    Console.WriteLine($"Message : {message}");

    //await connection.InvokeAsync("LogAsync", "Tamamlandı");
    return "Tamamlandı - Client Results";
});

while (true)
{
    if (Console.ReadKey().Key == ConsoleKey.M)
    {
        Console.WriteLine();
        Console.WriteLine("Lütfen mesajınızı göndereceğiniz connection Id değerini yazınız.");
        Console.Write("Connection ID : ");
        string connectionId = Console.ReadLine();
        Console.WriteLine("Lütfen göndereceğiniz mesajı yazınız.");
        Console.Write("Mesaj : ");
        string message = Console.ReadLine();
        Console.WriteLine();
        await connection.InvokeAsync("SendMessageAsync", message, connectionId);
    }
}