using MAUIPos.Application.Services;
using MAUIPos.Application.Views.Widget;
using CommunityToolkit.Maui.Core;
using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class WebSocketService
{
    private readonly AppConfigService _appConfig;
    private ClientWebSocket _webSocket;

    public WebSocketService(AppConfigService appConfig)
    {
        _appConfig = appConfig;
    }

    public async Task ConnectAsync()
    {
        try
        {
            if (_webSocket != null && _webSocket.State == WebSocketState.Open)
            {
                Console.WriteLine("WebSocket is already connected.");
                await DisconnectAsync();
            }

            _webSocket = new ClientWebSocket();
            Uri serverUri = new Uri(_appConfig.WS_SERVER_URL);
            await _webSocket.ConnectAsync(serverUri, CancellationToken.None);
            Console.WriteLine("Connected to WebSocket server!");
            
            // Start listening for incoming messages
            //_ = ReceiveMessagesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"WebSocket connection failed: {ex.Message}");
        }
    }

    public async Task GetDataAsync<T>(string message, Action<T>? callback = null)
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            try
            {
                await Task.Delay(1000);
                var tcs = new TaskCompletionSource<T>(); // This will be used to wait for the message
                _ = GetDataReceiveMessagesAsync(tcs); // This will populate tcs with the response when received
                var buffer = Encoding.UTF8.GetBytes(message);
                await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                T result = await tcs.Task; // This will block until the response is received
                callback?.Invoke(result);
            }
            catch (Exception ex)
            {
                await ToastWidget.ShowToast(ex.ToString(), ToastDuration.Long);
            }
        }
    }
   
    private async Task GetDataReceiveMessagesAsync<T>(TaskCompletionSource<T> tcs)
    {
        var buffer = new byte[1024];
        while (_webSocket.State == WebSocketState.Open)
        {
            var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                tcs.SetResult(default); // Complete the TaskCompletionSource with a default value
                Console.WriteLine("WebSocket closed.");
                return;
            }
            else
            {
                // Convert the message to a string
                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                // Deserialize the message to the desired type
                T obj = JsonConvert.DeserializeObject<T>(message);

                // Complete the TaskCompletionSource with the deserialized object
                tcs.SetResult(obj ?? default);

                // Exit the loop as we have received the message
                Console.WriteLine("Received: " + message);
                break;
            }
        }
    }

    public async Task SendMessageAsync(string message)
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }

    public async Task ReceiveMessagesObjectAsync<T>(Action<T>? callback = null)
    {
        var buffer = new byte[1024];
        while (_webSocket.State == WebSocketState.Open)
        {
            var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                callback?.Invoke(default);
            }
            else
            {
                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                T obj = JsonConvert.DeserializeObject<T>(message);
                callback?.Invoke(obj ?? default);
            }
        }
    }

    public async Task ReceiveMessagesStringAsync(Action<string>? callback = null)
    {
        var buffer = new byte[1024];
        while (_webSocket.State == WebSocketState.Open)
        {
            var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                callback?.Invoke(null);
            }
            else
            {
                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                callback?.Invoke(message);
            }
        }
    }

    public async Task ReceiveMessagesAsync()
    {
        byte[] buffer = new byte[1024];
        while (_webSocket.State == WebSocketState.Open)
        {
            var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Text)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Received message: {message}");
            }
        }
    }

    public async Task SubscribeObjectAsync<T>(string name, Action<T>? callback = null)
    {
        string subscribeMessage = $"/join {name}";
        await SendMessageAsync(subscribeMessage);
        await ReceiveMessagesObjectAsync(callback);
    }

    public async Task SubscribeStringAsync(string name, Action<string>? callback = null)
    {
        await ConnectAsync();
        string subscribeMessage = $"/join {name}";
        await SendMessageAsync(subscribeMessage);
        _ = ReceiveMessagesStringAsync(callback);
    }

    public async Task DisconnectAsync()
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Disconnecting", CancellationToken.None);
            Console.WriteLine("Disconnected from WebSocket server.");
        }
    }
}
