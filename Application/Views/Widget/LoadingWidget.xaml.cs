using MAUIPos.Application.Widget;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MAUIPos.Application.Views.Widget;
public partial class LoadingWidget : ContentView
{
    private CustomPopup? _popup;
    public LoadingWidget()
    {
        InitializeComponent();
        this.BindingContext = this;
        CreatePopup();
    }

    public static readonly BindableProperty LoadingTextProperty =
            BindableProperty.Create(nameof(LoadingText), typeof(string), typeof(LoadingWidget), "LOADING");

    public string LoadingText
    {
        get => (string)GetValue(LoadingTextProperty);
        set => SetValue(LoadingTextProperty, value);
    }   
    
    public static readonly BindableProperty IsCancelProperty =
            BindableProperty.Create(nameof(IsCancel), typeof(bool), typeof(LoadingWidget), false);

    public bool IsCancel
    {
        get => (bool)GetValue(IsCancelProperty);
        set => SetValue(IsCancelProperty, value);
    }

    private CancellationTokenSource _token = default;

    private bool _isloadingText = true;

    private async void CreatePopup()
    {
        _isloadingText = true;
        if (_popup == null)
            _popup = new CustomPopup(this);

        while (_isloadingText)
        {
            for (int i = 0; i <= 3; i++)
            {
                string text = $"Loading{new string('.', i)}".ToUpper();
                MainThread.BeginInvokeOnMainThread(() => LoadingText = text);
                await Task.Delay(1000);
            }
        }
    }

    public async Task ShowLoading(Func<CancellationToken, Task> task, bool isCancel = false, bool isDismissed = false, CancellationTokenSource token = default)
    {
        try
        {
            IsCancel = isCancel;
            _token = token ?? new CancellationTokenSource();
            if (!_popup.IsDisposed)
            {
                _popup.IsDisposed = true;
                _popup.CanBeDismissedByTappingOutsideOfPopup = isDismissed;
                App.Current.MainPage!.ShowPopup(_popup!);
                await task.Invoke(_token.Token);
            }
        }
        catch (TaskCanceledException)
        {
            await HideLoading();
        }
        catch (Exception ex)
        {
            _popup!.IsDisposed = false;
            _isloadingText = false;
            await ToastWidget.ShowToast(ex.ToString(), ToastDuration.Long);
        }
    }

    public async Task HideLoading()
    {
        try
        {
            _popup?.Close();
            _popup = null;
        }
        catch (Exception ex)
        {
            await ToastWidget.ShowToast(ex.ToString(), ToastDuration.Long);
        }
        finally
        {
            _isloadingText = false;
        }
    }

    private async void ButtonCancel_Clicked(object sender, EventArgs e)
    {
        if (_token != null && !_token.IsCancellationRequested)
        {
            _token.Cancel();
            _token.Dispose();
            _token = null;
        }
        await HideLoading();
    }

    //protected override bool OnBackButtonPressed()
    //{
    //    HideLoading();
    //    return true;
    //}
}
