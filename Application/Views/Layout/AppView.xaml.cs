using MAUIPos.Application.Services;
using MAUIPos.Application.Views.Layout;
using MAUIPos.Application.Views.Screen;
using MAUIPos.Application.Views.Widget;

namespace MAUIPos;

public partial class AppView : FlyoutPage
{
    private static AppView _instance;

    public static AppView Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AppView();
            }
            return _instance;
        }
    }

    Page MainPage
    {
        set
        {
            if (value != null)
            {
                Detail = value;
                OnPropertyChanged(nameof(Detail));
                OnPropertyChanged(nameof(MainPage));
            }
        }
    }
    public AppView()
    {
        InitializeComponent();
        Flyout = new FlyoutMenuPage(); // Replace with your actual Flyout menu

        // Set a temporary default Detail page
        Detail = new ContentPage { Content = new Label { Text = "Loading..." } };
        SetCurrentPagex();
    }

    public async void SetCurrentPagex()
    {
        try
        {
            IsPresented = false;
            var state = await MauiProgram.Services.GetService<AuthService>()!.GetToken();
            if (state == AuthenticationStatus.UnAuthenticated)
            {
                App.Current.MainPage = new LoginView();
            }
            else
            {
                App.Current.MainPage = MauiProgram.Services.GetService<AppShell>();
            }
            //if (!((IFlyoutPageController)this).ShouldShowSplitMode)
            //{
            //    MainThread.BeginInvokeOnMainThread(() =>
            //    {
            //        IsPresented = false;
            //    });
            //}
        }
        catch (Exception ex)
        {
            await ToastWidget.ShowToast(ex.ToString());
        }
    }

    //void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    var item = e.CurrentSelection.FirstOrDefault() as FlyoutPageItem;
    //    if (item != null)
    //    {
    //        Detail = new AppShell();

    //        // NavigationPage((Page)Activator.CreateInstance(item.TargetType));

    //        // Checking ShouldShowSplitMode is to fix the issue: https://github.com/dotnet/maui-samples/issues/219
    //        if (!((IFlyoutPageController)this).ShouldShowSplitMode)
    //        {
    //            MainThread.BeginInvokeOnMainThread(() =>
    //            {
    //                IsPresented = false;
    //            });
    //        }
    //    }
    //}

}