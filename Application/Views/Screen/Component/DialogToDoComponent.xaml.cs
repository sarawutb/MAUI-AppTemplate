using MAUIPos.Application.ViewModels;
using MAUIPos.Application.Views.Widget;
using CommunityToolkit.Maui.Views;

namespace MAUIPos.Application.Views.Screen.Component;

public partial class DialogToDoComponent : ContentView
{
    private readonly ToDoViewModel _view;
    private LoadingWidget _loading => MauiProgram.Services.GetService<LoadingWidget>()!;
    public DialogToDoComponent(ToDoViewModel mainViewModel)
    {
        InitializeComponent();
        _view = mainViewModel;
    }

    private void Close(object sender, EventArgs e)
    {
        (this.Parent as Popup)?.Close();
    }

    private async void Add(object sender, EventArgs e)
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
          {
              try
              {
                  if (!string.IsNullOrEmpty(TitleName.Text))
                  {
                      //_view.IsLoadToDo = true;
                      //_loading.ShowLoading();
                      (this.Parent as Popup)?.Close();
                      await Task.Delay(3000);
                      _view.LstTodoModel.Add(new Models.TodoModel
                      {
                          Title = TitleName.Text,
                          Status = true,
                      });
                      _view.LstTodoModel = _view.LstTodoModel;
                  }
              }
              catch (Exception ex)
              {
                  (this.Parent as Popup)?.Close();
                  await ToastWidget.ShowToast(ex.Message);
                  await SnackbarWidget.ShowSnackbar(ex.Message);
              }
              finally
              {
                  //_loading.HideLoading();
                  //_view.IsLoadToDo = false;
              }
          });
    }
}