using MAUIPos.Application.Views.Widget;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace MAUIPos.Application.Views.Screen;

public partial class MainView : ContentPage
{
    public MainView()
    {
        InitializeComponent();
        //collectionView.SelectionChanged += CollectionView_SelectionChanged;
    }

    protected override bool OnBackButtonPressed()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            try
            {
                if (Shell.Current.FlyoutIsPresented)
                {
                    Shell.Current.FlyoutIsPresented = false;
                }
                else
                {
                    bool state = await DialogWidget.DialogYesOrNo("����͹�к�", "�����ҵ�ͧ����͡�ҡ�к� ������� ?");
                    if (state)
                        App.Current.Quit();
                }
            }
            catch (Exception ex)
            {
                await ToastWidget.ShowToast(ex.ToString());
            }
        });
        return true;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        //var navState = Shell.Current.CurrentState;
        //if (navState.QueryParameters.TryGetValue("returnedData", out var data))
        //{
        //    string receivedData = data.ToString();
        //    Console.WriteLine($"Received: {receivedData}");
        //}
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (var item in ((CollectionView)sender).ItemsSource)
        {
            var visualElement = sender as CollectionView;
            //visualElement.BackgroundColor = e.CurrentSelection.Contains(item) ? Colors.LightBlue : Colors.Red;
        }
    }

}