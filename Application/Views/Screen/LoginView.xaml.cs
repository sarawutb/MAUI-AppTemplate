using MAUIPos.Application.Helper;
using MAUIPos.Application.Views.Widget;

namespace MAUIPos.Application.Views.Screen;

public partial class LoginView : ContentPage
{
    public LoginView()
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            bool state = await DialogWidget.DialogYesOrNo("����͹�к�", "�����ҵ�ͧ����͡�ҡ�к� ������� ?");
            if (state)
                App.Current.Quit();
        });
        return true;
    }

    protected override void OnAppearing()
    {
        StackLayoutLogo.Padding = ScreenHelper.SetMarginOrPadding(1, 2);
        ImageLogo.WidthRequest = ScreenHelper.SetScreenWidth(80); Console.WriteLine("test");
        base.OnAppearing();
    }
}