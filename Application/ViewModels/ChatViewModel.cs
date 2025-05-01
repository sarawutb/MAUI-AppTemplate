using MAUIPos.Application.Views.Screen;
using MAUIPos.Application.Views.Widget;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUIPos.Application.ViewModels
{
    public partial class ChatViewModel : BaseViewModel
    {
        public ICommand GoToRoomCommand => new RelayCommand<string>(async (roomName) => await OnGoToRoomAsync(roomName));
        public ICommand LoadRoomCommand => new RelayCommand(OnLoadRoom);

        //[ObservableProperty]
        //private ObservableCollection<string> lstRoom = new ObservableCollection<string>();
        private ObservableCollection<string> _lstRoom = new ObservableCollection<string>();
        public ObservableCollection<string> LstRoom
        {
            get => _lstRoom;
            set {
                _lstRoom = value;
                OnPropertyChanged();
            }
        }
        private readonly WebSocketService _webSocket;
        public ChatViewModel()
        {
            _webSocket = MauiProgram.Services.GetService<WebSocketService>();
            InitializationPage();
        }

        private async void InitializationPage()
        {
            await _webSocket.ConnectAsync();
            OnLoadRoom();
        }

        private async Task OnGoToRoomAsync(string? roomName)
        {
            try
            {
                await Route.GoToAsync<ChatRoomView>(
                    new Dictionary<string, object>
                    {
                        { "roomName", roomName! },
                    });
            }
            catch (Exception ex)
            {
                await SnackbarWidget.ShowSnackbar(ex.Message);
            }
        }

        private async void OnLoadRoom()
        {
            await _webSocket.GetDataAsync<List<string>>("/getListRoom", (data) =>
            {
                _lstRoom.Clear();
                for (int i = 0; i < data.Count; i++)
                {
                    _lstRoom.Add(data[i]);
                }
                LstRoom = _lstRoom;
            });
            //_webSocket.SendMessageAsync("/getListRoom");
            //MainThread.BeginInvokeOnMainThread(() =>
            //{
            //    for (int i = 1; i <= 10; i++)
            //    {
            //        lstRoom.Add("Room " + i.ToString());
            //    }
            //});
        }
    }
}
