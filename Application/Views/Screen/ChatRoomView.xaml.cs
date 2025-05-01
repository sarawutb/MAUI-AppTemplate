using MAUIPos.Application.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace MAUIPos.Application.Views.Screen;

[QueryProperty(nameof(_roomName), "roomName")]
public partial class ChatRoomView : ContentPage
{
    private readonly ChatRoomViewModel _viewModel;
    //[Parameter]
    public string _roomName
    {
        set
        {
            _viewModel.RoomName = value;
        }
    }   
    public ChatRoomView()
    {
        InitializeComponent();
        _viewModel = this.BindingContext as ChatRoomViewModel;
    }
}