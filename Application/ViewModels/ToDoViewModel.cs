using MAUIPos.Application.Models;
using MAUIPos.Application.Views.Screen.Component;
using MAUIPos.Application.Views.Widget;
using MAUIPos.Application.Widget;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MAUIPos.Application.ViewModels
{
    public partial class ToDoViewModel : BaseViewModel
    {
        public ICommand FilterEventCommand { get; }
        public ICommand NewEventCommand { get; }
        public ICommand DeleteEventCommand { get; }
        public ICommand DeleteEventSelectedCommand { get; }

        public ToDoViewModel()
        {
            FilterEventCommand = new RelayCommand(OnFilterEvent);
            NewEventCommand = new RelayCommand(OnNewEvent);
            DeleteEventCommand = new RelayCommand<TodoModel>(OnDeleteEvent);
            DeleteEventSelectedCommand = new RelayCommand(OnDeleteEventSeleted);
        }

        private async void OnDeleteEvent(TodoModel model)
        {
            var chk = await DialogWidget.DialogYesOrNo("แจ้งเตือนระบบ", $"แน่ใจว่าต้องการลบ {model.Title} ใช่หรือไม่ ?");
            if (chk)
            {
                _lstTodoModel.Remove(model);
                LstTodoModel = _lstTodoModel;
            }
        }

        private async void OnDeleteEventSeleted()
        {
            var chk = await DialogWidget.DialogYesOrNo("แจ้งเตือนระบบ", "แน่ใจว่าต้องการลบ ใช่หรือไม่ ?");
            if (chk)
            {
                LstTodoModel = _lstTodoModel.Where(s => s.Status == false).ToObservableCollection();
            }
        }

        public void OnFilterToDo(int type)
        {
            if (type == 1) LstTodoModel = _lstTodoModel.Where(a => a.Status == true).ToObservableCollection();
            else if (type == 2) LstTodoModel = new ObservableCollection<TodoModel>();
            else if (type == 3) LstTodoModel = _lstTodoModel.Where(a => a.Status == true).ToObservableCollection();
            _viewPopup?.Close();
        }

        private ObservableCollection<TodoModel> _lstTodoModel = TodoModelDataMock.lstData.ToObservableCollection();
        public ObservableCollection<TodoModel> LstTodoModel
        {
            get { return _lstTodoModel; }
            set
            {
                _lstTodoModel = value;
                OnPropertyChanged();
            }
        }

        public Popup _viewPopup { get; set; }

        private void OnFilterEvent()
        {
            _viewPopup = null;
            ObservableCollection<ItemSelection> _items = new()
            {
                new ItemSelection { Name = "All", Command = new Command(() =>
                {
                    OnFilterToDo(1);
                })},
                new ItemSelection { Name = "Show Completed", Command = new Command(() =>
                {
                    OnFilterToDo(2);
                })},
                new ItemSelection { Name = "All Active", Command = new Command(() =>
                {
                    OnFilterToDo(3);
                })
                }
            };
            _viewPopup = new CustomPopup(new PopUpToDoComponent(_items));
            App.Current.MainPage.ShowPopup(_viewPopup);
        }

        private bool _isLoadToDo;
        public bool IsLoadToDo
        {
            get => _isLoadToDo;
            set
            {
                _isLoadToDo = value;
                OnPropertyChanged();
            }
        }

        private async void OnNewEvent()
        {
            await App.Current.MainPage.ShowPopupAsync(new CustomPopup(new DialogToDoComponent(this)));
        }
    }
}
