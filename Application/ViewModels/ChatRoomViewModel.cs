using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace MAUIPos.Application.ViewModels
{
    public partial class ChatRoomViewModel : BaseViewModel
    {
        private string _roomName;
        private readonly WebSocketService _webSocket;

        public string RoomName
        {
            get => _roomName;
            set
            {
                _roomName = value;
                OnPropertyChanged();
            }
        }

        [ObservableProperty]
        private ObservableCollection<Message> messages = new ObservableCollection<Message>();

        [ObservableProperty]
        private string newMessage;

        public ICommand SendMessageCommand => new Command(async () => await SendMessageAsync());

        public ChatRoomViewModel()
        {
            _webSocket = MauiProgram.Services.GetService<WebSocketService>()!;
            InitializationPage();
        }

        // Changed to async Task for better exception handling
        private async void InitializationPage()
        {
            try
            {
                await _webSocket.ConnectAsync();
                //await _webSocket.SubscribeStringAsync($"/join {RoomName}", ReceiveMessage);
                await _webSocket.SendMessageAsync($"/join {RoomName}");
                _ = _webSocket.ReceiveMessagesStringAsync(ReceiveMessage);
                // Receive messages asynchronously and safely update the UI
            }
            catch (Exception ex)
            {
                // Handle errors, e.g., logging or alerting the user
                Console.WriteLine($"Error during initialization: {ex.Message}");
            }
        }

        // Send message asynchronously
        private async Task SendMessageAsync()
        {
            if (!string.IsNullOrWhiteSpace(NewMessage))
            {
                var _newMessage = NewMessage;
                var _newMessageObj = new Message { Text = NewMessage, IsUser = true };
                var _newMessageJson = JsonConvert.SerializeObject(_newMessageObj);
                Messages.Add(_newMessageObj);
                NewMessage = string.Empty;
                OnPropertyChanged(nameof(NewMessage));
                await _webSocket.SendMessageAsync(_newMessageJson);
            }
        }

        // Receive message safely on the UI thread
        private void ReceiveMessage(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                try
                {
                    string jsonString = message.Substring(message.IndexOf(":") + 1).Trim();
                    var _obj = JsonConvert.DeserializeObject<Message>(jsonString);
                    if (!_obj.IsUser)
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Messages.Add(_obj);
                        });
                }
                catch
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Messages.Add(new Message { Text = message, IsUser = false });
                    });
                }
            }
        }
    }

    public class Message
    {
        public string Text { get; set; }
        public bool IsUser { get; set; } // Determines if the message is from the user
    }
}
